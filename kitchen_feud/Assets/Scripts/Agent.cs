using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using UnityEngine.UIElements;

public class Agent : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Oven;
    public Tray tray;
    bool isMoving;
    public PlayerHolding playerHold;
    public PhotonView PV;
    public bool readyToServe;
    public GameObject agentTray;
    public bool served;
    private bool test = false;
    public bool goingToCollect;
    public bool goingToServe;
    public bool goingBack;
    public Vector3 initialPos;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHold = GetComponent<PlayerHolding>();
        Oven = GameObject.Find("Oven1").GetComponent<ovenMiniGame>().gameObject;
        PV = GetComponent<PhotonView>();
        readyToServe = false;
        int index = int.Parse(agent.name[6].ToString());
        if (agent.CompareTag("Waiter1"))
        {
            initialPos = GameSetup.GS.WSP1[index - 1].position;
        }
        else if (agent.CompareTag("Waiter2"))
        {
            initialPos = GameSetup.GS.WSP2[index - 1].position;


        }

    }

    // Update is called once per frame
    void Update()
    {
      

        if (PV.IsMine && PhotonNetwork.IsMasterClient)
        {
           
            if ((agent.transform.position - initialPos).magnitude < 0.8f)
            {
                agent.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            

            float dist = RemainingDistance(agent.path.corners);

            if (tray)
            {
                Vector3 trayPos = tray.transform.position;

                if (!goingToCollect)
                {
                    goingBack = false;
                    agent.SetDestination(new Vector3(trayPos.x, trayPos.y, trayPos.z - 2));
                    goingToCollect = true;
                }
        
                

                if (agent.remainingDistance != Mathf.Infinity  && agent.remainingDistance < 0.3f && agent.remainingDistance != 0 && 
                    agent.transform.position.x > trayPos.x - 1 && agent.transform.position.z > trayPos.z - 4)
                {
                    
                        agentTray = PhotonNetwork.Instantiate(Path.Combine("Appliances", "TrayPrefab"),
                            tray.transform.position,
                            tray.transform.rotation);
                        playerHold.pickUpItem(agentTray);
                        

                    



                    foreach (Transform t in tray.transform)
                    {

                        if (t.name.Contains("Slot") && t.childCount > 0)
                        {
                            agentTray.GetComponent<TraySlotting>().slotOnTray(t.GetChild(0).gameObject);                          
                            //playerHold.pickUpItem(t.GetChild(0).gameObject, t.GetChild(0).GetComponent<IngredientItem>().item);
                            //Set Tray to not Collectable;
                            
                            //break;
                        }

                    }
                    //tray.GetComponent<PhotonView>().RPC("setIsReadyF", RpcTarget.All, tray.GetComponent<PhotonView>().ViewID);
                    tray.isReady = false;
                    agent.ResetPath();
                    readyToServe = true;
                }



            }
            if (readyToServe)
            {
                float newDist = RemainingDistance(agent.path.corners);
                if (tray.SP && !goingToServe)
                {
                    Debug.LogError("Here");
                    agent.SetDestination(tray.SP.transform.position);
                    tray.SP = null;
                    goingToServe = true;
                    //tray.GetComponent<PhotonView>().RPC("setDestF",RpcTarget.All,tray.GetComponent<PhotonView>().ViewID);
                    
                }

                //Find Serving point and assign it to tray using 
                if (newDist < 1.3f && newDist != 0)
                {
                    foreach(Transform slot in agentTray.transform)
                    {
                        if (slot.transform.childCount>0)
                        {
                            PV.RPC("destroyAfterServed", RpcTarget.All, slot.transform.GetChild(0).GetComponent<PhotonView>().ViewID);
                        }
                    }
                    PhotonNetwork.Destroy(agentTray);
                    tray.Agent = null;
                    tray = null;
                    goingToCollect = false;
                    Debug.LogError("SetToFLASE");
                    //tray.GetComponent<PhotonView>().RPC("setAgentF", RpcTarget.All, tray.GetComponent<PhotonView>().ViewID);
                    //PV.RPC("setTrayNull", RpcTarget.All, PV.ViewID);

                    //tray.SP.GetPhotonView().RPC("setUsedF",RpcTarget.All,tray.SP.GetPhotonView().ViewID);
                    served = true;

                    readyToServe = false;
                    
                    
                    
                    

                }
            }

            if (served)
            {
                goingToServe = false;
                if (!goingBack)
                {
                    agent.SetDestination(initialPos);
                    //int index = int.Parse(agent.name[6].ToString());
                    //if (agent.CompareTag("Waiter1"))
                    //{
                    //    agent.SetDestination(GameSetup.GS.WSP1[index - 1].position);
                    //}else if (agent.CompareTag("Waiter2"))
                    //{
                    //    agent.SetDestination(GameSetup.GS.WSP2[index - 1].position);

                    //}
                    goingBack = true;
                    served = false;
                    
                }
               

            }
           
         

    

        }
    }

    public IEnumerator setD(NavMeshAgent agent, Vector3 targetpos)
    {
        yield return new WaitForSeconds(2);
        agent.SetDestination(targetpos);

    }


    public float RemainingDistance(Vector3[] points)
    {
        if (points.Length < 2) return 0;
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);
        return distance;
    }
    public void resetAgent()
    {
        PhotonNetwork.Destroy(agentTray);

        tray = null;
        readyToServe = false;
        served = false;
        goingBack = false;
        goingToCollect = false;
        goingToServe = false;
    }
    [PunRPC]
    void setTray(int agentID, int trayID) {

        PhotonView.Find(agentID).GetComponent<Agent>().tray = PhotonView.Find(trayID).GetComponent<Tray>();

    }
    [PunRPC]
    void setTrayNull(int agentID)
    {

        PhotonView.Find(agentID).GetComponent<Agent>().tray = null;

    }

    [PunRPC]
    void setAgentName(int viewID, string x)
    {
        PhotonView.Find(viewID).name = x;
    }
    [PunRPC]
    void destroyAfterServed(int viewID)
    {
        Destroy(PhotonView.Find(viewID).gameObject);
    }

}

