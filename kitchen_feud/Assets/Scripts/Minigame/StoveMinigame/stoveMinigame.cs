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
            }
        }
    }


   public void UpdateDishPointsStove() {
        Debug.Log("Called");
        if (appliance.isBeingInteractedWith){
            Debug.Log("Inside");
            Dish dishOfFoundDish = appliance.dishOfFoundDish;

            if(dishOfFoundDish != null)
            {
                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, 100);
                dishOfFoundDish.points = spawner.dishSO.maxScore * stoveScore.FinalMultipier();
                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
        }
    }
}
