using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine.U2D;
using UnityEngine.UI;

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
    public bool onPlate;
    public bool collided;
    public DishSO dishSO;
    public Image img;
    

    void Start()
    {
        img = GetComponent<Image>();
        collided = false;
        onPlate = false;
        minYSpeed = 200;
        maxYSpeed = 400;

        points = 0;
        PV = GetComponent<PhotonView>();

        RectTransform rect = GetComponent<RectTransform>();

    }

    void Update()
    {
        if (!dishSO)
        {
            dishSO = appliance.foundDish;
        }
        if(!img.sprite && dishSO && appliance)
        {
            img.sprite = appliance.GetComponent<fryingMinigame>().imgAtlas.GetSprite(dishSO.dishID);
        }
    }

    public void FlipPancake() {

        if (isFlipped == false) {
            isFlipped = true;
            timer.Reset();
            PV.RPC("setPoints",RpcTarget.All,PV.ViewID,timer.points);

            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 20f;
            Yspeed = Random.Range(minYSpeed, maxYSpeed);
            float ratio = maxYSpeed / Yspeed;
            float XSpeed = 300 / ratio;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-XSpeed,Yspeed);

            PV.RPC("flip",RpcTarget.All,PV.ViewID);
        }

    }

   [PunRPC]
    void flip(int viewID)
    {
        GameObject me = PhotonView.Find(viewID).gameObject;
        FriedFoodController FFC = me.GetComponent<FriedFoodController>();
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
            PhotonView.Find(viewID).GetComponent<FriedFoodController>().timer.Reset();
    }

    [PunRPC]
    void setPoints(int viewID,float points)
    {
        PhotonView.Find(viewID).GetComponent<FriedFoodController>().points = points;
    }
    [PunRPC]
    void destP(int viewID)
    {
        if (PhotonView.Find(viewID).gameObject)
        {
            Destroy(PhotonView.Find(viewID).gameObject);
        }
    }

    [PunRPC]
    void setParentPlate(int viewiD,int plateID) 
    {
        Plate plate = PhotonView.Find(plateID).GetComponent<Plate>();
        PhotonView.Find(viewiD).transform.SetParent(PhotonView.Find(plateID).transform);
        PhotonView.Find(viewiD).transform.localPosition = new Vector3(0, plate.currentH, PhotonView.Find(viewiD).transform.localPosition.z);
       
    }
}
