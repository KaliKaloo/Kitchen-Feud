using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Plate : MonoBehaviour
{
    public FriedFoodController friedFood;
    public List<FriedFoodController> stackedFood = new List<FriedFoodController>();
    public PanController pan;
    private Vector2 screenBounds;
    public float totalPoints;
    public Appliance appliance;
    public float speed = 10f;
    public FryingTimerBar timer;
    public PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        PV.RPC("setPlateStartVals", RpcTarget.All, PV.ViewID);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        timer = transform.parent.GetComponentInChildren<FryingTimerBar>();
    }

    void Update()
    {



        if (pan.friedFood != null)
        {
            friedFood = pan.friedFood;
            if (friedFood.gameObject.transform.position.x < screenBounds.x || friedFood.gameObject.transform.position.y < screenBounds.y)
            {
                PV.RPC("destP", RpcTarget.All, friedFood.GetComponent<PhotonView>().ViewID);

            }
        }
        if (appliance.appliancePlayers.Count > 1)
        {
            if (PhotonView.Find(appliance.appliancePlayers[1]).OwnerActorNr != GameObject.Find("Kitchen 1").GetPhotonView().OwnerActorNr)
            {
                GameObject.Find("Kitchen 1").GetPhotonView().TransferOwnership(PhotonView.Find(appliance.appliancePlayers[1]).Owner);
            }
            if (PV.Owner != PhotonView.Find(appliance.appliancePlayers[1]).Owner)
            {
                PV.TransferOwnership(PhotonView.Find(appliance.appliancePlayers[1]).Owner);
            }

            if (GameObject.Find("Local").GetComponent<PhotonView>().ViewID ==
                appliance.appliancePlayers[1] && GameObject.Find("Local").GetComponent<PhotonView>().IsMine)
            {

                Vector2 panPos = pan.transform.parent.gameObject.transform.parent.GetComponent<RectTransform>().position;
                Vector2 platePos = gameObject.GetComponent<RectTransform>().position;
                Vector3 EndPos = transform.parent.Find("End").transform.position;
                //movement: only if it's player2, rpcs for player1
                if (!(transform.position.x < EndPos.x))
                {

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {




                        transform.Translate(Vector3.left * 10 * 50 * Time.deltaTime);
                        //PV.RPC("moveLeft", RpcTarget.Others, PV.ViewID);


                    };

                }
                float panW = pan.transform.parent.GetComponent<RectTransform>().rect.width;
                if (platePos.x < panPos.x - panW)
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {


                        //

                        // Debug.LogError("ME: " + platePos.x + " Pan: " + panPos.x);

                        transform.Translate(Vector3.right * 10 * 50 * Time.deltaTime);
                        //Debug.LogError("Hello");
                        //PV.RPC("moveRight", RpcTarget.Others, PV.ViewID);

                    }

                }
            }
        }
    }
       
    
 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameObject.Find("Local").GetComponent<PhotonView>().ViewID ==
        appliance.appliancePlayers[1] && GameObject.Find("Local").GetComponent<PhotonView>().IsMine)
        {
            var obj = col.gameObject.GetComponent<FriedFoodController>();
            //totalPoints += obj.points;
            PV.RPC("syncTotal", RpcTarget.All, PV.ViewID, obj.points);
            GameEvents.current.assignPointsEventFunction();
            Debug.Log("total points:" + totalPoints);
            //obj.gameObject.transform.SetParent(this.gameObject.transform);
            Vector2 platePos = this.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
            obj.gameObject.GetComponent<RectTransform>().anchoredPosition = platePos;
            // stackedFood.Add(friedFood);
            Debug.Log("food destroyed");
            PV.RPC("destP", RpcTarget.All, col.GetComponent<PhotonView>().ViewID);
            //Destroy(obj.gameObject);
            //friedFood = null;
        }
    }
    [PunRPC]
    void setPlateStartVals(int viewID)
    {
        Plate me = PhotonView.Find(viewID).GetComponent<Plate>();
        //me.appliance = me.transform.parent.parent.GetComponent<Appliance>();
        me.pan = transform.parent.Find("PanGameObject").GetComponentInChildren<PanController>();
        me.totalPoints = 0;
        me.friedFood = me.pan.friedFood;
        //me.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    [PunRPC]
    void moveLeft(int viewID)
    {
        PhotonView.Find(viewID).transform.Translate(Vector3.left * 10 * 45 * Time.deltaTime);
        //PhotonView.Find(viewID).GetComponent<Rigidbody2D>().MovePosition(Vector2.left);
    }
    [PunRPC]
    void moveRight(int viewID)
    {
        PhotonView.Find(viewID).transform.Translate(Vector3.right * 10 * 45 * Time.deltaTime);
        //PhotonView.Find(viewID).GetComponent<Rigidbody2D>().MovePosition(Vector2.right);
        //PhotonView.Find(viewID).transform.GetComponent<RectTransform>().anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        //PhotonView.Find(viewID).transform.position = transform.position;
        //Debug.Log(transform.GetComponent<RectTransform>().anchoredPosition);
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
    void syncTotal(int viewiD, float points)
    {
        PhotonView.Find(viewiD).GetComponent<Plate>().totalPoints += points;
    }
}