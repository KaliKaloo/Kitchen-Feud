using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 


public class IngredientSpawner : Interactable
{
    public GameObject ingredientPrefab;

    public override void Interact()
    {
        Debug.Log("interacted with spawner");
        PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        Transform slot = playerHold.slot;
        if (playerHold.items.Count ==0){
            var obj = PhotonNetwork.Instantiate(ingredientPrefab.name, slot.position, Quaternion.identity);
            pickableItem item = obj.GetComponent<pickableItem>();
            playerHold.pickUpItem(obj, item.item);
        }
    }
}
