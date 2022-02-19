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
                Debug.Log(ts.tray.trayID);
                break;
            }
        }
    }

    public void resetTray(string orderid)
    {
        foreach (GameObject t in trays)
        {
            Tray ts = t.GetComponent<Tray>();

            if (ts.tray.trayID == orderid)
            {
                ts.tray.trayID = "";

                // if tray matches order add to score
                CompareOrder(ts.tray.ServingTray, ts.tray.objectsOnTray, orderid);

                ts.tray.ServingTray.Clear();
                ts.tray.objectsOnTray.Clear();
                foreach (Transform slot in t.transform){
                    //Debug.Log(slot.name);

                    foreach(Transform child in slot){
                        Destroy(child.gameObject);
                    }
                    
                }
                break;
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
            Debug.Log(dishComponent.points);
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

        print(String.Join("; ", trayNames));
        print(String.Join("; ", dishNames));

        if (trayNames.SequenceEqual(dishNames))
            return true;
        else
            return false;
    }

    // compares a tray to an orderid
    private void CompareOrder(List<BaseFood> tray, List<GameObject> onTray, string orderid)
    {
        Order o = Database.GetOrderByID(orderid);
        int currentScore = 0;

        // Compares two dishes without order mattering (now checks for duplicates too)
        bool temp = CompareDishNames(tray, o.dishes);
        print(temp);
        if (temp)
        {
            currentScore += GetDishScore(onTray);
        }
        // deduct scores if they contain raw ingredients
        //currentScore += IngredientDeduction(tray);

        if (teamNumber == 1)
        {
            this.GetComponent<PhotonView>().RPC("UpdateScore1", RpcTarget.All, currentScore);
        } else if (teamNumber == 2)
        {
            this.GetComponent<PhotonView>().RPC("UpdateScore2", RpcTarget.All, currentScore);
        }
        
    }

    private void OnApplicationQuit() {
        foreach(GameObject t in trays){
            Tray ts = t.GetComponent<Tray>();
            ts.tray.trayID = "";
            ts.tray.ServingTray.Clear();
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
    void resetAcross(string orderID)
    {
        resetTray(orderID);
    }
}
