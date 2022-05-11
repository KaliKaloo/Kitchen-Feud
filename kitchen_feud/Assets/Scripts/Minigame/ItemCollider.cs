using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Photon.Pun;

// controller for when an item collides with a collider
public class ItemCollider : MonoBehaviour
{
    private GameObject parentObject;
    private Appliance parentAppliance;

    void Start()
    {
        parentObject = gameObject.transform.parent.gameObject;
        parentAppliance = parentObject.GetComponent<Appliance>();
    }

    // On entering collider
    void OnTriggerEnter(Collider collision)
    {
        // if collides with an ingredient
        if (collision.CompareTag("Ingredient") && collision.transform.parent)
        {
            // set all player values for other scripts
            GameObject player = collision.transform.parent.parent.gameObject;
            PhotonView pv = player.GetComponent<PhotonView>();
            parentAppliance.GetComponent<PhotonView>().RPC("setPlayer", RpcTarget.AllBuffered, parentAppliance.GetComponent<PhotonView>().ViewID,
                pv.ViewID);

            PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
            parentAppliance.playerRigidbody = player.GetComponent<Rigidbody>();
            parentAppliance.SlotsController = parentObject.GetComponent<SlotsController>();

            // slot into appliance if not locked
            if (pv.IsMine && parentAppliance.canUse && playerHold && !playerHold.itemLock)
            {
                globalClicked.applianceInteract = true;
                parentObject.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.AllBuffered, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                                    player.GetComponent<PhotonView>().ViewID);
            }
        }
    }
}
