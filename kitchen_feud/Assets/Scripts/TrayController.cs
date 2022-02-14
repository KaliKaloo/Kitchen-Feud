using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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

    private int CompareIngredients(List<BaseFood> tray, List<BaseFood> orderDish)
    {
        List<BaseFood> orderIngredients;
        foreach (BaseFood food in orderDish)
        {
            // NEED ACCESS TO RECIPE
            //food.item
        }
        return 0;
    }

    // compares a tray to an orderid
    private void CompareOrder(List<BaseFood> tray, string orderid)
    {
        Order o = Database.GetOrderByID(orderid);
        int currentScore = 0;

        // Compares two dishes without order mattering (now checks for duplicates too)
        if (Enumerable.SequenceEqual(tray.OrderBy(t => t), o.dishes.OrderBy(t => t)))
        {

            currentScore += GetDishScore(tray);
        }
        // if contains 100% of ingredients only then multiply dish score by 0.25
        else if (CompareIngredients(tray, o.dishes) == 2)
        {
            currentScore += (int)(GetDishScore(tray) * 0.25);
        } // if less than 100% of ingredients only then multiply dish score by 0.1
        else if (CompareIngredients(tray, o.dishes) == 1)
        {
            currentScore += (int)(GetDishScore(tray) * 0.1);
        }

        // IF PLAYER PART OF TEAM 1
        if (true)
        {
            scores.AddScore1(currentScore);
        }
        // IF PLAYER PART OF TEAM 2
        //else if (false)
        //{
        //    scores.AddScore2(GetDishScore(o.dishes));
        //}
    }

    private void OnApplicationQuit() {
        foreach(GameObject t in trays){
            Tray ts = t.GetComponent<Tray>();
            ts.tray.trayID = "";
            ts.tray.ServingTray.Clear();
        }
    }
}
