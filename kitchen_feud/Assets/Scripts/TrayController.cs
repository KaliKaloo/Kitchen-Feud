using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrayController : MonoBehaviour
{
    public List<GameObject> trays = new List<GameObject>();

    private static ParseScore scores = new ParseScore();

    public void makeTray(string orderID){
        foreach (GameObject t in trays){
            Tray ts = t.GetComponent<Tray>();
            string trayID = ts.tray.trayID;
            if(ts.tray.trayID == ""){
                ts.tray.trayID = orderID;
                Debug.Log(ts.tray.trayID);
                break;
            }
        }
    }

    public void resetTray(string orderid){
        foreach(GameObject t in trays){
            Tray ts = t.GetComponent<Tray>();

            if(ts.tray.trayID == orderid){
                ts.tray.trayID = "";

                // if tray matches order add to score
                CompareOrder(ts.tray.ServingTray, orderid);

                ts.tray.ServingTray.Clear();
                break;
            }
        }
    }

    // gets the full max score of an order
    private int GetDishScore(List<BaseFood> trayDishes)
    {
        int total = 0;

        foreach(BaseFood dish in trayDishes)
        {
            // Make sure to change to FINAL SCORE after karolina has figured out how to deduct points.
            total += dish.maxScore;
        }

        return total;
    }

    // compares a tray to an orderid
    private void CompareOrder(List<BaseFood> tray, string orderid)
    {
        Order o = Database.GetOrderByID(orderid);
        // change lists to hash sets so order doesn't matter
        // RIGHT NOW TRAY AND DISH ARE DIFFERENT TYPES
        // CANNOT COMPARE
        List<BaseFood> trayList = new List<BaseFood>(tray);
        List<BaseFood> orderList = new List<BaseFood>(o.dishes);
        trayList.Sort();
        orderList.Sort();
        print(trayList);
        print(orderList);

        // Compares two dishes without order mattering

        if (orderList.Equals(trayList)) {

            // IF PLAYER PART OF TEAM 1
            if (true)
            {
                scores.AddScore1(GetDishScore(tray));
            }
            // IF PLAYER PART OF TEAM 2
            //else if (false)
            //{
            //    scores.AddScore2(GetDishScore(o.dishes));
            //}
        }
    }

    private void OnApplicationQuit() {
        foreach(GameObject t in trays){
            Tray ts = t.GetComponent<Tray>();
            ts.tray.trayID = "";
            ts.tray.ServingTray.Clear();
        }
    }
}
