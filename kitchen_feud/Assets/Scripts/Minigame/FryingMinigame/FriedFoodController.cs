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
    public float minYSpeed;
    public float maxYSpeed;
    public GameObject gameCanvas;
    public Appliance appliance;

//use dishSO to get sprite and set it as the friedFood image
    public DishSO dishSO;

    void Start()
    {   
        gameObject.transform.SetParent(pan.gameObject.transform.parent);
        gameObject.transform.localPosition = Vector3.zero;
        points = 0;
        PV = GetComponent<PhotonView>();
        appliance = transform.parent.parent.parent.GetComponent<Appliance>();
    }

    void Update()
    {

    }

    public void FlipPancake() {

        if (isFlipped == false) {
            isFlipped = true;
            Debug.Log("Flip pancake!");
            points = timer.Reset();
            //I shouldn't assign points yet, only if it's the last call. maybe keep a counter?
            if(points != 0) GameEvents.current.assignPointsEventFunction();

            //this.YeetPancake();
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
        Debug.LogError("hello");
        GameObject me = PhotonView.Find(viewID).gameObject;
        FriedFoodController FFC = me.GetComponent<FriedFoodController>();
        me.GetComponent<Rigidbody2D>().gravityScale = 1f;
        //me.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(FFC.minXSpeed, FFC.maxXSpeed), Random.Range(FFC.minYSpeed, FFC.maxYSpeed));
        me.GetComponent<Rigidbody2D>().velocity = new Vector2(-60,60);
        me.transform.SetParent(FFC.gameCanvas.transform);
        Vector2 panPos = FFC.pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        me.GetComponent<RectTransform>().anchoredPosition = panPos;
    }
}
