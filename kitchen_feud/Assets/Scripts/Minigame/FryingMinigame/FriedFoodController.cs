using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FriedFoodController : MonoBehaviour
{
    public PanController pan;
    public PhotonView PV;
    public Vector3 startLocation;
    public bool isFlipped = false;
    public FryingTimerBar timer;
    public float points;
    public float minXSpeed;
    public float maxXSpeed;
    public float Yspeed;
    public float minYSpeed;
    public float maxYSpeed;
    public GameObject gameCanvas;
    public Appliance appliance;

//use dishSO to get sprite and set it as the friedFood image
    public DishSO dishSO;

    void Start()
    {
        minYSpeed = 200;
        maxYSpeed = 400;
        gameObject.transform.SetParent(pan.gameObject.transform.parent);
        gameObject.transform.localPosition = Vector3.zero;
        points = 0;
        PV = GetComponent<PhotonView>();
        appliance = transform.parent.GetComponentInChildren<PanController>().appliance;
        //do in RPC
        //PV.RPC("setDishSO", RpcTarget.All, PV.ViewID);
        dishSO = appliance.foundDish;
        //appliance = transform.parent.parent.parent.GetComponent<Appliance>();
    }

    void Update()
    {
        if (!dishSO)
        {
            dishSO = appliance.foundDish;
        }
    }

    public void FlipPancake() {

        if (isFlipped == false) {
            isFlipped = true;
            Debug.Log("Flip pancake!");
            timer.Reset();
            //points = timer.points;
            PV.RPC("setPoints",RpcTarget.All,PV.ViewID,timer.points);
            Debug.LogError(points);
            //I shouldn't assign points yet, only if it's the last call. maybe keep a counter?
           // if(points != 0) GameEvents.current.assignPointsEventFunction();

            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 20f;
            //this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
            Yspeed = Random.Range(minYSpeed, maxYSpeed);
            float ratio = maxYSpeed / Yspeed;
            float XSpeed = 300 / ratio;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-XSpeed,Yspeed);

            //YeetPancake();
            PV.RPC("flip",RpcTarget.All,PV.ViewID);
        }

    }
    
    public void YeetPancake() {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));

        this.gameObject.transform.SetParent(gameCanvas.transform);
        Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = panPos;
        
    }
   [PunRPC]
    void flip(int viewID)
    {
        GameObject me = PhotonView.Find(viewID).gameObject;
        FriedFoodController FFC = me.GetComponent<FriedFoodController>();
        //me.GetComponent<Rigidbody2D>().gravityScale = 1f;
        //me.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(FFC.minXSpeed, FFC.maxXSpeed), Random.Range(FFC.minYSpeed, FFC.maxYSpeed));
        //me.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(FFC.minXSpeed, FFC.maxXSpeed), Random.Range(FFC.minYSpeed, FFC.maxYSpeed));
        me.transform.SetParent(FFC.gameCanvas.transform);
        Vector2 panPos = FFC.pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        me.GetComponent<RectTransform>().anchoredPosition = panPos;
    }
    [PunRPC]
    void setParentP(int viewID)
    {
        PhotonView.Find(viewID).transform.SetParent(PhotonView.Find(viewID).GetComponent<FriedFoodController>().gameCanvas.transform);
    }
    [PunRPC]
    void resetTimerRPC(int viewID)
    {
        //PhotonView.Find(viewID).GetComponent<FriedFoodController>().points =
            PhotonView.Find(viewID).GetComponent<FriedFoodController>().timer.Reset();
    }
    [PunRPC]
    void setDishSO(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<FriedFoodController>().dishSO = PhotonView.Find(viewID).GetComponent<FriedFoodController>().appliance.foundDish;
    }
    [PunRPC]
    void setPoints(int viewID,float points)
    {
        PhotonView.Find(viewID).GetComponent<FriedFoodController>().points = points;
    }
}
