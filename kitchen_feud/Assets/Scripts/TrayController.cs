using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Photon.Pun;

public class TrayController : MonoBehaviour
{
    public List<GameObject> trays = new List<GameObject>();

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
    private int GetDishScore(List<GameObject> trayDishes)
    {
        int total = 0;

        foreach(GameObject dish in trayDishes)
        {
            // Make sure to change to FINAL SCORE after karolina has figured out how to deduct points.
            Dish dishComponent = dish.GetComponent<Dish>();
            total += (int)dishComponent.points;
           
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
    private bool CompareDishNames(List<BaseFood> tray, List<BaseFood> orderDish)
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

        if (trayNames.SequenceEqual(dishNames))
            return true;
        else
            return false;
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
                bool temp = CompareDishNames(tray, o.dishes);

                if (temp)
                {
                    currentScore += GetDishScore(onTray);
                }
                // deduct scores if they contain raw ingredients
                //currentScore += IngredientDeduction(tray);

                if (teamNumber == 1)
                {
                    this.GetComponent<PhotonView>().RPC("UpdateScore1", RpcTarget.All, currentScore);
                }
                else if (teamNumber == 2)
                {
                    this.GetComponent<PhotonView>().RPC("UpdateScore2", RpcTarget.All, currentScore);
                }
                // resetTray(orderid);
            }

            this.GetComponent<PhotonView>().RPC("resetAcross", RpcTarget.All, ts.GetComponent<PhotonView>().ViewID);
            break;
        }
        
    }

    private void OnApplicationQuit() {
        foreach(GameObject t in trays){
            Tray ts = t.GetComponent<Tray>();
            ts.tray.trayID = null;
            ts.tray.ServingTray.Clear();
            ts.tray.objectsOnTray.Clear();
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
