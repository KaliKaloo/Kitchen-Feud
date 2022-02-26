using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Photon.Pun;

public class TrayController : MonoBehaviour
{
    public List<GameObject> trays = new List<GameObject>();
    public List<GameObject> otherTrays = new List<GameObject>();
    //public GlobalTimer timer;
    private static ParseScore scores = new ParseScore();

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
            if (slot.childCount != 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }
    }

    // gets the full max score of an order
    private int GetDishScore(List<GameObject> trayDishes, List<BaseFood> trayItems)
    {
        int total = 0;

        foreach(GameObject dish in trayDishes)
        {
            // Make sure to change to FINAL SCORE after karolina has figured out how to deduct points.
            try{
                Dish dishComponent = dish.GetComponent<Dish>();
                total += (int)dishComponent.points;
            }catch{
                total += IngredientDeduction(trayItems);
            }
           
        }

        return total;
    }


    // returns -points based on how many raw ingredients on tray
    private int IngredientDeduction(List<BaseFood> tray)
    {
        int total = 0;

        foreach (BaseFood food in tray)
        {
            if (food.Type == ItemType.Ingredient)
            {
                total += food.maxScore;
            }
        }
        return total; 
    }

    // purely compares an order and a tray based on their names
    private float CompareDishNames(List<BaseFood> tray, List<BaseFood> orderDish)
    {
        List<string> trayNames = new List<string>();
        List<string> dishNames = new List<string>();

        foreach (BaseFood food in tray)
        {
            trayNames.Add(food.name);
        }
        foreach (BaseFood food in orderDish)
        {
            dishNames.Add(food.name);
        }
        // sort dishes alphabetically
        trayNames = trayNames.OrderBy(q => q).ToList();
        dishNames = dishNames.OrderBy(q => q).ToList();

        List<string> commonNames = new List<string>();
        var holdCommon = trayNames.Intersect(dishNames);
        
        if (holdCommon.Count() > 0)
                holdCommon.ToList().ForEach(t => commonNames.Add(t));

        return ((float)commonNames.Count()/(float)dishNames.Count());
    }

    private int calcScore(List<GameObject> trayDishes, List<BaseFood> trayItems, List<BaseFood> orderDish){
        int total = 0;
        List<BaseFood> tempOrderDish = orderDish;

        for(int i =0; i<trayItems.Count(); i++)
        {
            if (trayItems[i].Type == ItemType.Ingredient)
            {
                total += trayItems[i].maxScore;
            }
            else if (tempOrderDish.Any(dish => dish.name == trayItems[i].name)){
                Dish dishComponent = trayDishes[i].GetComponent<Dish>();
                total += (int)dishComponent.points;
                tempOrderDish.Remove(trayItems[i]);
            }
        }
        return total; 
    }

    // compares a tray to an orderid
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

                // Compares two dishes without order mattering (now checks for duplicates too)
                // float dishMultiplier = CompareDishNames(tray, o.dishes);
                // float totalPoints = GetDishScore(onTray, tray) * dishMultiplier;
                // currentScore += (int)totalPoints;

                currentScore += calcScore(onTray, tray, o.dishes);

                if (teamNumber == 1)
                {
                    this.GetComponent<PhotonView>().RPC("UpdateScore1", RpcTarget.All, currentScore);
                }
                else if (teamNumber == 2)
                {
                    this.GetComponent<PhotonView>().RPC("UpdateScore2", RpcTarget.All, currentScore);
                }
                // resetTray(orderid);
                this.GetComponent<PhotonView>().RPC("resetAcross", RpcTarget.All, ts.GetComponent<PhotonView>().ViewID);
                break;
            }

        }
        
    }

    private void OnApplicationQuit() {
        for(int i = 0; i< trays.Count; i++)
        {
            //PhotonNetwork.Disconnect();   
            Tray ts = trays[i].GetComponent<Tray>();
            Tray oTs = otherTrays[i].GetComponent<Tray>();
            ts.tray.trayID = null;
            ts.tray.ServingTray.Clear();
            ts.tray.objectsOnTray.Clear();
            oTs.tray.trayID = null;
            oTs.tray.ServingTray.Clear();
            oTs.tray.objectsOnTray.Clear();
        }
     /*   foreach(GameObject t in trays){
            Tray ts = t.GetComponent<Tray>();
            ts.tray.trayID = null;
            ts.tray.ServingTray.Clear();
            ts.tray.objectsOnTray.Clear();
        }
     */
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
