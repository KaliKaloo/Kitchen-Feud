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
    private int canvasTag;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsOven;
        appliance = GetComponent<Appliance>();

        
    }
    
    void Update()
    {
        if (transform.GetComponentInChildren<Timer>())
        {
            timer = transform.GetComponentInChildren<Timer>();
            backbutton = timer.GetComponentInChildren<exitOven>();

            if (appliance.isBeingInteractedWith)
            {
                backbutton.appliance = GetComponent<Appliance>();
                canvasTag = appliance.kitchenNum;
 

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

                if (appliance.foundDish)
                {
                    int ingredientMultiplier = appliance.foundDish.recipe.Count - 1;

                    dishOfFoundDish.points = timer.score + (30 * ingredientMultiplier);

                    // if player is team 2 but interacts with team1 stove, points doubled
                    if (appliance.kitchenNum == 1&& (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
                        dishOfFoundDish.points = dishOfFoundDish.points * 2;

                    // if player is team 1 but interacts with team2 stove, points doubled
                    else if (appliance.kitchenNum ==2 && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
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
