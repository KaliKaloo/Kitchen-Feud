using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PanController : MonoBehaviour
{
    public Transform pan;
    public float clampDistance = Screen.width/5;
    public Vector3 startLocation;
    public float mouseCursorSpeed;
    
    //the fried food needs to depend on the dish
    public FriedFoodController friedFood;
    public Appliance appliance;
    public PhotonView PV;
    public GameObject friedFoodPrefab;
    public int foodInstances = 5;
    public float speedLimit = 1000;
    public int speedQueueCapacity = 50;
    private Queue<float> speeds;
    private float avgSpeeds;
    private bool haveAvg;
    public int foodInstancesCounter;
    public FryingTimerBar timer;
    private bool pointsAssigned = false;
    public GameObject temp;

    void Start () {
        PV = GetComponent<PhotonView>();
        pan = transform.parent;
        startLocation = pan.position;
        haveAvg = false;
        speeds = new Queue<float>();
        foodInstancesCounter = 0;

        Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        if (PV.IsMine)
        {
             temp = PhotonNetwork.Instantiate(Path.Combine("Minigames", "Pancake"), panPos, friedFoodPrefab.transform.rotation);
            PV.RPC("setFoodVals", RpcTarget.All, temp.GetComponent<PhotonView>().ViewID, PV.ViewID);
        }


        
    
    }

    void Update()
    {
        if(appliance.appliancePlayers.Count > 0) { 
        if (PhotonView.Find(appliance.appliancePlayers[0]).OwnerActorNr != PV.OwnerActorNr)
        {
           PV.TransferOwnership(PhotonView.Find(appliance.appliancePlayers[0]).Owner);
        }
            //if (appliance.appliancePlayers.Count > 0 && PhotonView.Find(appliance.appliancePlayers[0]).OwnerActorNr != GameObject.Find("Kitchen 1").GetPhotonView().OwnerActorNr)
            //{
            //    GameObject.Find("Kitchen 1").GetPhotonView().TransferOwnership(PhotonView.Find(appliance.appliancePlayers[0]).Owner);
            //}
            //do that all if it's player1, add rpcs to player2
            if (GameObject.Find("Local").GetComponent<PhotonView>().ViewID ==
             appliance.appliancePlayers[0] && GameObject.Find("Local").GetComponent<PhotonView>().IsMine)
            {
                mouseCursorSpeed = abs(Input.GetAxis("Mouse X") / Time.deltaTime);
                if (speeds.Count == speedQueueCapacity)
                {

                    while (speeds.Count > 0)
                    {
                        avgSpeeds += speeds.Dequeue();
                    }
                    avgSpeeds = avgSpeeds / speedQueueCapacity;
                    haveAvg = true;

                    speeds.Clear();
                    //Debug.Log(avgSpeeds);
                }
                else
                {
                    speeds.Enqueue(mouseCursorSpeed);
                }


                Vector3 lastLocation = pan.position;

                if (Input.GetAxis("Mouse X") < 0 && (startLocation.x - lastLocation.x < clampDistance || startLocation.x < lastLocation.x)
                    && friedFood.appliance.appliancePlayers.Count > 1)
                {
                    //PV.RPC("movePan", RpcTarget.All, PV.ViewID, mouseCursorSpeed, 0);
                    pan.Translate(Vector3.left * mouseCursorSpeed * 2 * Time.deltaTime);
                }
                if (Input.GetAxis("Mouse X") > 0 && (lastLocation.x - startLocation.x < clampDistance || startLocation.x > lastLocation.x)
                    && friedFood.appliance.appliancePlayers.Count > 1)
                {
                    //PV.RPC("movePan", RpcTarget.All, PV.ViewID, mouseCursorSpeed, 1);
                    pan.Translate(Vector3.right * mouseCursorSpeed * 2 * Time.deltaTime);
                    if (avgSpeeds > speedLimit && haveAvg == true && pointsAssigned == false)
                    {
                        pointsAssigned = true;
                        friedFood.FlipPancake();
                    }
                }

                if (foodInstancesCounter < foodInstances && friedFood == null)
                {
                    Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
                    var temp = PhotonNetwork.Instantiate(Path.Combine("Minigames", "Pancake"), panPos, friedFoodPrefab.transform.rotation);
                    PV.RPC("setFoodVals", RpcTarget.All, temp.GetComponent<PhotonView>().ViewID, PV.ViewID);
                    foodInstancesCounter++;
                    pointsAssigned = false;
                    Debug.Log(foodInstancesCounter);
                }

            }
        }
    }
    public float abs(float x) {
       float result = x < 0 ? -x : x;
       return result;
    }
    [PunRPC]
    void movePan(int viewID,float mouseSpeed,int dir)
    {
        PanController pan = PhotonView.Find(viewID).GetComponent<PanController>();
        if (dir == 0)
        {
            pan.pan.Translate(Vector3.left * mouseSpeed * 2 * Time.deltaTime);
        }
        else
        {
            pan.pan.Translate(Vector3.right * mouseSpeed * 2 * Time.deltaTime);
        }
    }
    [PunRPC]
    void setFoodVals(int viewID,int myID)
    {
        FriedFoodController FFC;
        GameObject me;
        me = PhotonView.Find(myID).gameObject;
        FFC = PhotonView.Find(viewID).GetComponent<FriedFoodController>();
        me.GetComponent<PanController>().friedFood = FFC;
        FFC.pan = me.GetComponent<PanController>();
        FFC.gameCanvas = me.transform.parent.transform.parent.gameObject;
        FFC.timer = me.GetComponent<PanController>().timer;

    }

  
}