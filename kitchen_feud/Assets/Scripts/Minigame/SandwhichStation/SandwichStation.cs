using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SandwichStation: MonoBehaviour
{
    public SandwichController SandwichController;
    private Appliance appliance;
    public ExitSandwichMinigame backbutton;
    

    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsSandwich;
        appliance = GetComponent<Appliance>();

    }
    void Update()
    {
        if (appliance.isBeingInteractedWith)
        {

            if (appliance.player && appliance.player.GetComponent<PhotonView>().IsMine)
            {
                backbutton.appliance = GetComponent<Appliance>();

                if (appliance.foundDish != null)
                {
                    SandwichController.dish = appliance.foundDish;
                }
            }

        }
    }


    //CALLED BY THE EVENT SYSTEM
    public void UpdateDishPointsSandwich()
    {
        if (appliance.isBeingInteractedWith)
        {
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if (dishOfFoundDish != null)
            {

                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, SandwichController.finalScore);
                dishOfFoundDish.points = SandwichController.finalScore;
                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
            else
            {
                Debug.Log("dishoffounddish is null");
            }
        }

    }


}
