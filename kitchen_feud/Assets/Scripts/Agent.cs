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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHold = GetComponent<PlayerHolding>();
        Oven = GameObject.Find("Oven1").GetComponent<ovenMiniGame>().gameObject;
        PV = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
       
        if (tray)
        {
            agent.SetDestination(tray.transform.position);
            float dist = RemainingDistance(agent.path.corners);

            if (dist < 1.3f && dist != 0)
            {
                foreach (Transform t in tray.transform)
                {
                    
                    if (t.name.Contains("Slot") && t.childCount > 0)
                    {
                        playerHold.pickUpItem(t.GetChild(0).gameObject, t.GetChild(0).GetComponent<IngredientItem>().item);
                        agent.ResetPath();
                        //GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                        PV.RPC("setTrayNull", RpcTarget.All, PV.ViewID);
                        break;
                    }

                }
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

