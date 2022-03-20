using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ovenMiniGame : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject oven;
    public Timer timer;
    private Appliance appliance;
    public exitOven backbutton;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsOven;
        appliance = GetComponent<Appliance>();
    }
    
    void Update()
    {
        if (transform.childCount == 6)
        {
            timer = transform.GetChild(5).GetComponent<Timer>();
            backbutton = transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<exitOven>();

            if (appliance.isBeingInteractedWith)
            {
                backbutton.appliance = GetComponent<Appliance>();
            }
        }
        if(gameObject.GetComponentInChildren<ParticleSystem>().isPlaying){
            appliance.canUse = false;

        } else{
            appliance.canUse = true;
        }
    }


    //CALLED BY THE EVENT SYSTEM
    public void UpdateDishPointsOven()
    {
       if (appliance.isBeingInteractedWith)
        {
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if (dishOfFoundDish != null)
            {

                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, timer.score);
                dishOfFoundDish.points = timer.score;

                // if player is team 2 but interacts with team1 stove, points doubled
                if (GetComponent<Appliance>().kitchenNum == 1 && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
                    dishOfFoundDish.points = dishOfFoundDish.points * 2;
                // if player is team 1 but interacts with team2 stove, points doubled
                else if (GetComponent<Appliance>().kitchenNum == 2 && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
                    dishOfFoundDish.points = dishOfFoundDish.points * 2;

                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
            else
            {
                Debug.Log("dishoffounddish is null");
            }
        }

    }

}
