using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SandwichStation: MonoBehaviour
{
    public GameObject sandwichCanvas;
    public SandwichController SandwichController;
    private Appliance appliance;
    public ExitSandwichMinigame backbutton;
    

    void Start()
    {
        appliance = GetComponent<Appliance>();
    }

    void Update()
    {
        if (appliance.isBeingInteractedWith && appliance.player && appliance.player.GetComponent<PhotonView>().IsMine)
        {
            SandwichController.SandwichStation = GetComponent<SandwichStation>();
            MusicManager.instance.minigameSwitch();
		    MusicManager.instance.inMG = true;
            backbutton.appliance = GetComponent<Appliance>();

            if (appliance.foundDish != null)
            {
                SandwichController.dish = appliance.foundDish;
            }
        }
    }


    public void UpdateDishPointsSandwich()
    {
        if (appliance.isBeingInteractedWith)
        {
        
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if (dishOfFoundDish != null)
            {

                if (appliance.foundDish)
                {
                    int ingredientMultiplier = appliance.foundDish.recipe.Count - 1;

                    dishOfFoundDish.points = SandwichController.finalScore + (30 * ingredientMultiplier);

                    // if player is team 2 but interacts with team1 appliance, points doubled
                    if (sandwichCanvas.tag == "Team1" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
                        dishOfFoundDish.points = dishOfFoundDish.points * 2;

                    // if player is team 1 but interacts with team2 appliance, points doubled
                    else if (sandwichCanvas.tag == "Team2" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
                        dishOfFoundDish.points = dishOfFoundDish.points * 2;

                    dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, (int)dishOfFoundDish.points);


                }
            }
            else
            {
                Debug.Log("dishoffounddish is null");
            }
        }

    }
}
