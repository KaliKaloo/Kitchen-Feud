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

    public PhotonPlayer player_random;

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
        // deduct scores if they contain raw ingredients
        currentScore += IngredientDeduction(tray);

        currentScore = 100;

        Debug.LogError(player_random.myTeam);

        if (player_random.myTeam == 0)
        {
            this.GetComponent<PhotonView>().RPC("UpdateScore1", RpcTarget.Others, currentScore);
            scores.AddScore1(currentScore);
        } else
        {
            this.GetComponent<PhotonView>().RPC("UpdateScore2", RpcTarget.Others, currentScore);
            scores.AddScore2(currentScore);
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
}
