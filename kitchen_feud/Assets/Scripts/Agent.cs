using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Agent : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Oven;
    public Tray tray;
    bool isMoving;
    public PlayerHolding playerHold;
    public PhotonView PV;
    public bool readyToServe;

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
            test = true;
            float dist = RemainingDistance(agent.path.corners);

            if (tray)
            {
                Debug.Log(tray.name);
                agent.SetDestination(tray.transform.position);
                 
                if (dist < 1.3f && dist != 0)
                {


                    foreach (Transform t in tray.transform)
                    {

                        if (t.name.Contains("Slot") && t.childCount > 0)
                        {
                            playerHold.pickUpItem(t.GetChild(0).gameObject, t.GetChild(0).GetComponent<IngredientItem>().item);
                            //Set Tray to not Collectable;
                            tray.GetComponent<PhotonView>().RPC("setIsReadyF", RpcTarget.All, tray.GetComponent<PhotonView>().ViewID);
                            agent.ResetPath();
                            readyToServe = true;
                            break;
                        }

                    }
                }



            }
            if (readyToServe)
            {
                float newDist = RemainingDistance(agent.path.corners);
                if (tray.SP)
                {
                    agent.SetDestination(tray.SP.transform.position);
                }

                //Find Serving point and assign it to tray using 
                if (newDist < 1.3f && newDist != 0)
                {
                    tray.GetComponent<PhotonView>().RPC("setAgentF", RpcTarget.All, tray.GetComponent<PhotonView>().ViewID);
                    PV.RPC("setTrayNull", RpcTarget.All, PV.ViewID);
                    //tray.SP.GetPhotonView().RPC("setUsedF",RpcTarget.All,tray.SP.GetPhotonView().ViewID);

                    readyToServe = false;

                }
            }


            //agent.SetDestination(GameObject.Find("Local").transform.position);
            //if (Oven.GetComponentInChildren<Timer>())
            //{
            //    if (Oven.GetComponentInChildren<Timer>().timerFake == 35)
            //    {
            //        agent.SetDestination(Oven.GetComponentInChildren<Timer>().transform.position);
            //        isMoving = true;

            //    }

            //    float dist = RemainingDistance(agent.path.corners);


            //    if (dist <0.3 && dist !=0)
            //    {
            //        Debug.LogError("s");
            //        Oven.GetComponentInChildren<Timer>().GetComponentInChildren<exitOven>().TaskOnClick();
            //        if (Oven.GetComponent<Appliance>().cookedDish)
            //        {
            //            GetComponent<PlayerHolding>().pickUpItem(Oven.GetComponent<Appliance>().cookedDish,
            //                Oven.GetComponent<Appliance>().cookedDish.GetComponent<pickableItem>().item);
            //        }
            //        isMoving = false;
            //    }


            //}
            //  if(GetComponent<PlayerHolding>().slot.childCount != 0)
            //    {
            //        agent.SetDestination(GameObject.Find("k1").GetComponentInChildren<Tray>().transform.position);
            //    }

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

}

