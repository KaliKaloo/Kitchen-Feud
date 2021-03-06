using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PanController : MonoBehaviour
{
    public Transform pan;
    public float clampDistance = Screen.width/5;
    public Vector3 startLocation;
    public float mouseCursorSpeed;
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
    public bool iniialValsSet;
    public fryingMinigame fM;
    public GameObject backButton;

    void Start () {

        PV = GetComponent<PhotonView>();
        pan = transform.parent;
        startLocation = pan.position;
        haveAvg = false;
        speeds = new Queue<float>();
        foodInstancesCounter = 0;
        Vector2 panPos = pan.gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;

        if (PV.IsMine)
        {
             temp = PhotonNetwork.Instantiate(Path.Combine("Minigames", "Pancake"), panPos, friedFoodPrefab.transform.rotation);
            PV.RPC("setFoodVals", RpcTarget.AllBuffered, temp.GetComponent<PhotonView>().ViewID, PV.ViewID);
        }
    
    }

    void Update()
    {
        //setting initial values of the fryingMinigame script attached to appliance
        if(appliance && !iniialValsSet)
        {
            fM = appliance.GetComponent<fryingMinigame>();
            fM.gameCanvas = transform.parent.parent.gameObject;

            fM.pan = GetComponent<PanController>();
        }

        if(appliance.appliancePlayers.Count > 0) {
        //checking if the first player to click on the appliance is the owner of the Pan
        if (PhotonView.Find(appliance.appliancePlayers[0]).OwnerActorNr != PV.OwnerActorNr)
        {
           PV.TransferOwnership(PhotonView.Find(appliance.appliancePlayers[0]).Owner);
        }
            if (GameObject.Find("Local").GetComponent<PhotonView>().ViewID ==
             appliance.appliancePlayers[0] && GameObject.Find("Local").GetComponent<PhotonView>().IsMine)
            {
                //using the mouse movement to flip the item
                mouseCursorSpeed = Mathf.Abs(Input.GetAxis("Mouse X") / Time.deltaTime);
                if (speeds.Count == speedQueueCapacity)
                {

                    while (speeds.Count > 0)
                    {
                        avgSpeeds += speeds.Dequeue();
                    }
                    avgSpeeds = avgSpeeds / speedQueueCapacity;
                    haveAvg = true;

                    speeds.Clear();
                }
                else
                {
                    speeds.Enqueue(mouseCursorSpeed);
                }


                Vector3 lastLocation = pan.position;

                if (Input.GetAxis("Mouse X") < 0 && (startLocation.x - lastLocation.x < clampDistance || startLocation.x < lastLocation.x)
                    && appliance.appliancePlayers.Count > 1)
                {
                    pan.Translate(Vector3.left * mouseCursorSpeed * 5 * Time.deltaTime);
                }
                if (Input.GetAxis("Mouse X") > 0 && (lastLocation.x - startLocation.x < clampDistance || startLocation.x > lastLocation.x)
                    && appliance.appliancePlayers.Count > 1)
                {
                    pan.Translate(Vector3.right * mouseCursorSpeed * 5 * Time.deltaTime);
                    if (avgSpeeds > speedLimit && haveAvg == true && pointsAssigned == false)
                    {
                        pointsAssigned = true;
                        friedFood.FlipPancake();
                    }
                }

                if (foodInstancesCounter < foodInstances && friedFood == null)
                {
                    //Instantiating a new item when a previous one has been flipped
                    Vector2 panPos = pan.gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
                    var temp = PhotonNetwork.Instantiate(Path.Combine("Minigames", "Pancake"), panPos, friedFoodPrefab.transform.rotation);
                    PV.RPC("setFoodVals", RpcTarget.AllBuffered, temp.GetComponent<PhotonView>().ViewID, PV.ViewID);
                    pointsAssigned = false;
                    
                }

                if (foodInstancesCounter == 5)
                {
                    PV.RPC("enableBack", RpcTarget.All, PV.ViewID);
                }

            }
        }
    }

    [PunRPC]
    void setFoodVals(int viewID,int myID)
    {
        //setting values of the item to be flipped
        FriedFoodController FFC;
        GameObject me;
        Appliance appliance;
        SpriteAtlas imgAtlas;

        me = PhotonView.Find(myID).gameObject;
        appliance = me.GetComponent<PanController>().appliance;
        FFC = PhotonView.Find(viewID).GetComponent<FriedFoodController>();
        appliance.GetComponent<fryingMinigame>().friedFoodController = FFC;
        FFC.appliance = appliance;
        FFC.transform.SetParent(me.transform);
        FFC.transform.localPosition = Vector3.zero;
        imgAtlas = appliance.GetComponent<fryingMinigame>().imgAtlas;
        FFC.dishSO = appliance.foundDish;
        if (imgAtlas && FFC.dishSO)
        {
            FFC.GetComponent<Image>().sprite = imgAtlas.GetSprite(FFC.dishSO.dishID);
        }
        me.GetComponent<PanController>().friedFood = FFC;
        FFC.pan = me.GetComponent<PanController>();
        FFC.gameCanvas = me.transform.parent.transform.parent.gameObject;
        FFC.timer = me.GetComponent<PanController>().timer;
        
       
   

    }

    [PunRPC]
    void enableBack(int viewID)
    {
        //enabling the back button for the frying minigame
        PhotonView.Find(viewID).GetComponent<PanController>().backButton.gameObject.SetActive(true);
    }
  
}
