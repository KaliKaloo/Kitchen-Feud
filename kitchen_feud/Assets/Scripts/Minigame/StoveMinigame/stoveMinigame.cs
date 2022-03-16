using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun; 

public class stoveMinigame : MonoBehaviour
{
    [SerializeField] public GameObject stoveCanvas;
    [SerializeField] public GameObject startButton;

    StoveScore stoveScore = new StoveScore();

    public Spawner spawner;

    private Appliance appliance;
    public ExitStoveMinigame backbutton;

    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsStove;
        appliance = GetComponent<Appliance>();
        backbutton.gameObject.SetActive(false);
    }

    void Update(){
        if(appliance.isBeingInteractedWith && appliance.player && appliance.player.GetComponent<PhotonView>().IsMine)
        {
            backbutton.appliance = GetComponent<Appliance>();
            if (appliance.foundDish != null)
            {
                spawner.dishSO = appliance.foundDish;
                spawner.appliance = appliance;
            }
        }
    }


   public void UpdateDishPointsStove() {
        if (appliance.isBeingInteractedWith){
            Dish dishOfFoundDish = appliance.dishOfFoundDish;

            if(dishOfFoundDish != null)
            {
                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, 100);
                dishOfFoundDish.points = spawner.dishSO.maxScore * stoveScore.FinalMultipier();

                // if player is team 2 but interacts with team1 stove, points doubled
                if (stoveCanvas.tag == "Team1" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
                    dishOfFoundDish.points = dishOfFoundDish.points * 2;
                // if player is team 1 but interacts with team2 stove, points doubled
                else if (stoveCanvas.tag == "Team2" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
                    dishOfFoundDish.points = dishOfFoundDish.points * 2;

                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
        }
    }
}
