using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 


public class IngredientSpawner : Interactable
{
    public GameObject ingredientPrefab;
    private int count = 20;
    private static GlobalTimer timer = new GlobalTimer();
    private int totalTime = timer.GetTotalTime();

    private bool reset;

   
    protected override void Update(){

    
        base.Update();
        int currentTime = timer.GetTime();
        if (currentTime <= totalTime/2 && !reset){
            GetComponent<PhotonView>().RPC("resetCount", RpcTarget.All);
        }
    }


    public override void Interact()
    {
        
        PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        Transform slot = playerHold.slot;
        if (playerHold.items.Count == 0 && count > 0){
            var obj = PhotonNetwork.Instantiate(ingredientPrefab.name, slot.position, Quaternion.identity);
            pickableItem item = obj.GetComponent<pickableItem>();
            playerHold.pickUpItem(obj, item.item);
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
