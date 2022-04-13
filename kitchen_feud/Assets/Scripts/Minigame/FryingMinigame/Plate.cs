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
    public PhotonView localPV;
    public int controllerPV;
    public float currentH;


    void Start()
    {
        PV = GetComponent<PhotonView>();
        PV.RPC("setPlateStartVals", RpcTarget.AllBuffered, PV.ViewID);
       // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        timer = transform.parent.GetComponentInChildren<FryingTimerBar>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 0.21f, Screen.height * 0.37f);
        RectTransform rect = GetComponent<RectTransform>();
        GetComponent<CapsuleCollider2D>().size = new Vector2(rect.rect.width/1.4f, (rect.rect.height / 6.15f) + 100);
        GetComponent<CapsuleCollider2D>().offset = new Vector2(GetComponent<CapsuleCollider2D>().offset.x, GetComponent<CapsuleCollider2D>().size.y/3);
    }

    void Update()
    {
        if (appliance.appliancePlayers.Count > 1)
        {
        
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
                    };

                }
                float panW = pan.transform.parent.GetComponent<RectTransform>().rect.width;
                if (platePos.x < panPos.x)
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        transform.Translate(Vector3.right * 10 * 50 * Time.deltaTime);
  
                    }

                }
            }
        }
    }
       
    
 
    void OnTriggerEnter2D(Collider2D col)
    {
        
        var obj = col.gameObject.GetComponent<FriedFoodController>();

        if (!obj.collided)
        {
            if (GameObject.Find("Local").GetComponent<PhotonView>().ViewID ==
            appliance.appliancePlayers[1] && GameObject.Find("Local").GetComponent<PhotonView>().IsMine)
            {

            }
            else
            {
                PV.RPC("syncTotal", RpcTarget.AllBuffered, PV.ViewID, obj.points);

                Debug.Log("total points:" + totalPoints);

       



                col.GetComponent<Rigidbody2D>().gravityScale = 0;
                col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                col.GetComponent<PhotonView>().RPC("setParentPlate", RpcTarget.AllBuffered, col.GetComponent<PhotonView>().ViewID, PV.ViewID);
            }
            obj.collided = true;

            currentH += 22.5f;
            col.GetComponent<FriedFoodController>().onPlate = true;
            pan.friedFood = null;
            pan.foodInstancesCounter += 1;
            
        }
    }
    [PunRPC]
    void setPlateStartVals(int viewID)
    {
        Plate me = PhotonView.Find(viewID).GetComponent<Plate>();
        me.pan = transform.parent.Find("PanGameObject").GetComponentInChildren<PanController>();
        me.totalPoints = 0;
        me.friedFood = me.pan.friedFood;
    }
    [PunRPC]
    void moveLeft(int viewID)
    {
        PhotonView.Find(viewID).transform.Translate(Vector3.left * 10 * 45 * Time.deltaTime);
    }
    [PunRPC]
    void moveRight(int viewID)
    {
        PhotonView.Find(viewID).transform.Translate(Vector3.right * 10 * 45 * Time.deltaTime);

    }
  
    [PunRPC]
    void syncTotal(int viewiD, float points)
    {
        PhotonView.Find(viewiD).GetComponent<Plate>().totalPoints += points;
    }
}
