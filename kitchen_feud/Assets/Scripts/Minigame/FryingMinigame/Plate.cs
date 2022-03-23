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

            if (GameObject.Find("Local").GetComponent<PhotonView>().ViewID ==
            appliance.appliancePlayers[1] && GameObject.Find("Local").GetComponent<PhotonView>().IsMine)
            {
                //movement: only if it's player2, rpcs for player1


                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (!(transform.position.x < screenBounds.x)) {

                        PV.RPC("moveLeft", RpcTarget.All, PV.ViewID);
                        transform.Translate(Vector3.left * 10 * 30 * Time.deltaTime);

                            };
            
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
                    Vector2 platePos = gameObject.GetComponent<RectTransform>().anchoredPosition;
                    //
                    if (platePos.x + 370 < panPos.x)
                    {
                        PV.RPC("moveRight", RpcTarget.All, PV.ViewID);

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
        me.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    [PunRPC]
    void moveLeft(int viewID)
    {
        PhotonView.Find(viewID).transform.Translate(Vector3.left * 10 * 30 * Time.deltaTime);
    }
    [PunRPC]
    void moveRight(int viewID)
    {
        PhotonView.Find(viewID).transform.Translate(Vector3.right * 10 * 30 * Time.deltaTime);
    }
    [PunRPC]
    void destP(int viewID)
    {
        Destroy(PhotonView.Find(viewID).gameObject);
    }
    void syncTotal(int viewiD, float points)
    {
        PhotonView.Find(viewiD).GetComponent<Plate>().totalPoints += points;
    }
}
