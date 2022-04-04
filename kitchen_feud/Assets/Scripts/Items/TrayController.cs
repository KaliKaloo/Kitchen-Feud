using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Photon.Pun;
using TMPro;

public class TrayController : MonoBehaviour
{
    public List<GameObject> trays = new List<GameObject>();
    public List<GameObject> otherTrays = new List<GameObject>();
    private static ParseScore scores = new ParseScore();

    private void Start()
    {
        for (int i = 0; i < trays.Count; i++) {
            trays[i].GetComponent<Tray>().tray.objectsOnTray.Clear();
            otherTrays[i].GetComponent<Tray>().tray.objectsOnTray.Clear();
        }
    }

    public int teamNumber;
    public void makeTray(string orderID){
        foreach (GameObject t in trays){
            Tray ts = t.GetComponent<Tray>();
            string trayID = ts.tray.trayID;
            if (ts.tray.trayID == "")
            {
                ts.tray.trayID = orderID;
                break;
            }
        }
    }

    public void resetTray(Tray ts)
    {
        ts.tray.trayID = "";

        ts.tray.ServingTray.Clear();
        ts.tray.objectsOnTray.Clear();

        foreach (Transform slot in ts.transform)
        {
            // overwrite order stand prefab
            if (slot.tag == "OrderTower")
            {
                slot.GetComponentInChildren<TextMeshProUGUI>().text ="";
            }

            // else destroy items on tray, except from item collider
            if (slot.childCount != 0 && slot.tag != "ItemCollider" && slot.tag != "OrderTower")
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }
    }

    private int calcScore(List<GameObject> trayDishes, List<BaseFood> trayItems, List<BaseFood> orderDish){
        int total = 0;
        List<BaseFood> tempOrderDish = new List<BaseFood>(orderDish);

        for (int i = 0; i < trayItems.Count(); i++)
        {

            if (trayItems[i].Type == ItemType.Ingredient)
            {
                total += trayItems[i].maxScore;
            }
            else if (tempOrderDish.Any(dish => dish.name == trayItems[i].name))
            {

                Dish dishComponent = trayDishes[i].GetComponent<Dish>();
                total += (int)dishComponent.points;
                Debug.Log((int)dishComponent.points);
                tempOrderDish.Remove(trayItems[i]);
            }
            else
            {
                Debug.Log("this is the dish" + trayItems[i].name);
                foreach (BaseFood item in tempOrderDish)
                {
                    Debug.Log(item.name);
                }
            }
        }
        float difference = trayItems.Count() - orderDish.Count();
        if(difference>0){
            total = (int)(total - 3*(difference*0.9f));
        }
        return total; 
    }

    public void CompareOrder(string orderid)
    {
        foreach (GameObject t in trays)
        {
            Tray ts = t.GetComponent<Tray>();

            if (ts.tray.trayID == orderid)
            {
                List<BaseFood> tray = ts.tray.ServingTray;
                List<GameObject> onTray = ts.tray.objectsOnTray;

                Order o = Database.GetOrderByID(orderid);
                int currentScore = 0;

                if(tray.Count>0)
                    currentScore += calcScore(onTray, tray, o.dishes);

                if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
                {
                    this.GetComponent<PhotonView>().RPC("UpdateScore1", RpcTarget.All, currentScore);
                }
                else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
                {
                    this.GetComponent<PhotonView>().RPC("UpdateScore2", RpcTarget.All, currentScore);
                }
     
                this.GetComponent<PhotonView>().RPC("resetAcross", RpcTarget.All, ts.GetComponent<PhotonView>().ViewID);
                break;
            }

        }
        
    }

    private void OnApplicationQuit() {
        for(int i = 0; i< trays.Count; i++)
        {
           
            Tray ts = trays[i].GetComponent<Tray>();
            Tray oTs = otherTrays[i].GetComponent<Tray>();
            ts.tray.trayID = null;
            ts.tray.ServingTray.Clear();
            ts.tray.objectsOnTray.Clear();
            oTs.tray.trayID = null;
            oTs.tray.ServingTray.Clear();
            oTs.tray.objectsOnTray.Clear();
        }

    }
   

    [PunRPC]
    void UpdateScore1(int score)
    {
        scores.AddScore1(score);
    }

    [PunRPC]
    void UpdateScore2(int score)
    {
        scores.AddScore2(score);
    }
    [PunRPC]
    void makeTrayAcross(string orderID) {
        makeTray(orderID);
    }
    [PunRPC]
    void resetAcross(int viewID)
    {
        resetTray(PhotonView.Find(viewID).GetComponent<Tray>());
    }
}
