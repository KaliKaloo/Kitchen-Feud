using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;

public class IngredientSpawner : Interactable
{
    
    public GameObject ingredientPrefab;
    private int count = 180;
    private static GlobalTimer timer = new GlobalTimer();
    private int totalTime = timer.GetTotalTime();
    private bool timerSet, reset;

   
    protected override void Update(){
        if (timer.GetTotalTime() != 0 && !timerSet)
        {
            totalTime = timer.GetTotalTime();
            timerSet = true;
        }
        base.Update();
        int currentTime = timer.GetLocalTime();
        if (currentTime <= totalTime/2 && !reset){
            GetComponent<PhotonView>().RPC("resetCount", RpcTarget.All);
        }
    }


    public override void Interact()
    {
        
        PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        Transform slot = playerHold.slot;
        if (player.transform.Find("slot").childCount== 0 && count > 0){
            var obj = PhotonNetwork.Instantiate(Path.Combine( "IngredientPrefabs", ingredientPrefab.name), slot.position, Quaternion.identity);
            pickableItem item = obj.GetComponent<pickableItem>();
            playerHold.pickUpItem(obj);
            GetComponent<PhotonView>().RPC("decCount", RpcTarget.All);
        }
    }

    [PunRPC]
    void decCount()
    {
        count -= 1;
    }

    [PunRPC]
    void resetCount()
    {
        count = 20;
        reset = true;
    }
}
