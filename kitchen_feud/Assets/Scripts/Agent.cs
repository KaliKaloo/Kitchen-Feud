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
    
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            agent = GetComponent<NavMeshAgent>();
            playerHold = GetComponent<PlayerHolding>();
            Oven = GameObject.Find("Oven1").GetComponent<ovenMiniGame>().gameObject;
            
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
    }

    void Update()
    {
      

        if (PV.IsMine && PhotonNetwork.IsMasterClient)
        {

            fixRotation();

            if (tray)
            {
                foundTray();
            }

            if (readyToServe)
            {

                collectedDish();
            }

            if (served)
            {
                returnToWaitingPos();
            
            }
           
         

    

        }
    }
    void collectedDish() {

   
       

            float newDist = RemainingDistance(agent.path.corners);
            if (tray.SP && !goingToServe)
            {
                agent.ResetPath();
                agent.SetDestination(tray.SP.transform.position);
                tray.SP = null;
                goingToServe = true;

            }

            //Find Serving point and assign it to tray using 
            if (newDist < 1.3f && newDist != 0)
            {
                foreach (Transform slot in agentTray.transform)
                {
                    if (slot.transform.childCount > 0)
                    {
                        PV.RPC("destroyAfterServed", RpcTarget.All, slot.transform.GetChild(0).GetComponent<PhotonView>().ViewID);
                    }
                }
                PhotonNetwork.Destroy(agentTray);
                tray.Agent = null;
                tray = null;
                goingToCollect = false;

                served = true;

                readyToServe = false;
            }

    }

    void returnToWaitingPos()
    {
        goingToServe = false;
        if (!goingBack)
        {
            agent.SetDestination(initialPos);

            goingBack = true;
            served = false;

        }
    }
    void foundTray()
    {

       
            Vector3 trayPos = tray.transform.position;

            if (!goingToCollect)
            {
                goingBack = false;
                agent.SetDestination(new Vector3(trayPos.x, trayPos.y, trayPos.z - 2));
                goingToCollect = true;
            }



            if (agent.remainingDistance != Mathf.Infinity && agent.remainingDistance < 0.3f && agent.remainingDistance != 0 &&
           (agent.transform.position - trayPos).magnitude < 2.5f)
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

                    }

                }
                tray.isReady = false;
                agent.ResetPath();
                readyToServe = true;
            }
        
    }
    void fixRotation()
    {
        if ((agent.transform.position - initialPos).magnitude < 0.8f)
        {
            agent.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
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

