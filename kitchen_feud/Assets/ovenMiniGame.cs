using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ovenMiniGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Timer timer;
    private Appliance appliance;
    public exitOven backbutton;
    //public int finalPoints;

    //public cuttingkeypress k;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsOven;
        appliance = GetComponent<Appliance>();
        Debug.LogError("These are the points" + timer.score);

    }
    
    void Update()
    {
        Debug.LogError(appliance.isBeingInteractedWith);
        if (appliance.isBeingInteractedWith)
        {

           // if (appliance.player && appliance.player.GetComponent<PhotonView>().IsMine)
            {

                backbutton.appliance = GetComponent<Appliance>();

            }

        }
    }


    //CALLED BY THE EVENT SYSTEM
    public void UpdateDishPointsOven()
    {
        Debug.LogError("TEST");
        if (appliance.isBeingInteractedWith)
        {
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            Debug.LogError("FOUND DISH? " + appliance.dishOfFoundDish.name);
            if (dishOfFoundDish != null)
            {

                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, timer.score);
                dishOfFoundDish.points = timer.score;
                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
            else
            {
                Debug.Log("dishoffounddish is null");
            }
        }

    }

}
