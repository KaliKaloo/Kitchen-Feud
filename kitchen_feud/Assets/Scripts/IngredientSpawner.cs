using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 


public class IngredientSpawner : Interactable
{
    public GameObject ingredientPrefab;
    private int count = 20;
   

    public override void Interact()
    {
        
        PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        Transform slot = playerHold.slot;
        if (playerHold.items.Count ==0){
            var obj = PhotonNetwork.Instantiate(ingredientPrefab.name, slot.position, Quaternion.identity);
            pickableItem item = obj.GetComponent<pickableItem>();
            playerHold.pickUpItem(obj, item.item);
            count -= 1;
        }

        Debug.Log(count);
    }
}
