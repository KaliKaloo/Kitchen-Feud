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
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHold = GetComponent<PlayerHolding>();
        Oven = GameObject.Find("Oven1").GetComponent<ovenMiniGame>().gameObject;
        PV = GetComponent<PhotonView>();
        readyToServe = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            /*if (transform.CompareTag("Waiter1") && test == false)
            {
                Debug.LogError("SET?");
                agent.SetDestination(GameObject.Find("Tray1 (1)").transform.position);
                test = true;
            }

            if (dist < 1.3f && dist != 0)
            {
                playerHold.pickUpItem(GameObject.Find("Tray1 (1)"), GameObject.Find("Tray1 (1)").GetComponent<IngredientItem>().item);
            }*/
           
            float dist = RemainingDistance(agent.path.corners);

            if (tray)
            {
                if (!agent.hasPath)
                {

                    agent.SetDestination(tray.transform.position);
                }

                if (dist < 1.3f && dist != 0 && !agentTray)
                {
                    
                        agentTray = PhotonNetwork.Instantiate(Path.Combine("Appliances", "TrayPrefab"),
                            tray.transform.position,
                            tray.transform.rotation);
                        playerHold.pickUpItem(agentTray, null);

                    



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
                    tray.GetComponent<PhotonView>().RPC("setIsReadyF", RpcTarget.All, tray.GetComponent<PhotonView>().ViewID);
                    agent.ResetPath();
                    readyToServe = true;
                }



            }
            if (readyToServe)
            {
                float newDist = RemainingDistance(agent.path.corners);
                if (tray.SP && !agent.hasPath)
                {
                    agent.SetDestination(tray.SP.transform.position);
                    tray.GetComponent<PhotonView>().RPC("setDestF",RpcTarget.All,tray.GetComponent<PhotonView>().ViewID);
                    
                }

                //Find Serving point and assign it to tray using 
                if (newDist < 1.3f && newDist != 0)
                {
                    tray.GetComponent<PhotonView>().RPC("setAgentF", RpcTarget.All, tray.GetComponent<PhotonView>().ViewID);
                    PV.RPC("setTrayNull", RpcTarget.All, PV.ViewID);
                    
                    //tray.SP.GetPhotonView().RPC("setUsedF",RpcTarget.All,tray.SP.GetPhotonView().ViewID);
                    
                    readyToServe = false;
                    if(agentTray){PhotonNetwork.Destroy(agentTray);}
                    
                    served = true;
                    

                }
            }

            if (served)
            {
                if (!agent.hasPath)
                {
                    agent.SetDestination(GameSetup.GS.WSP1[0].position);
                }
                float newDist = RemainingDistance(agent.path.corners);
                float dist1 = agent.remainingDistance;


                if (dist1 != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0 )
                {
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
    [PunRPC]
    void setTray(int agentID, int trayID) {

        PhotonView.Find(agentID).GetComponent<Agent>().tray = PhotonView.Find(trayID).GetComponent<Tray>();

    }
    [PunRPC]
    void setTrayNull(int agentID)
    {

        PhotonView.Find(agentID).GetComponent<Agent>().tray = null;

    }

}

