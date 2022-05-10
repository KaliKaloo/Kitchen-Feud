using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Photon.Pun;

public class ItemCollider : MonoBehaviour
{
    private GameObject parentObject;
    private Appliance parentAppliance;

    void Start()
    {
        parentObject = gameObject.transform.parent.gameObject;
        parentAppliance = parentObject.GetComponent<Appliance>();
    }

    //On trigger slot item to appliance
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ingredient") && collision.transform.parent)
     
        {
            GameObject player = collision.transform.parent.parent.gameObject;
            PhotonView pv = player.GetComponent<PhotonView>();
            parentAppliance.GetComponent<PhotonView>().RPC("setPlayer", RpcTarget.AllBuffered, parentAppliance.GetComponent<PhotonView>().ViewID,
                pv.ViewID);

            PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
            parentAppliance.playerRigidbody = player.GetComponent<Rigidbody>();
            parentAppliance.SlotsController = parentObject.GetComponent<SlotsController>();

            if (pv.IsMine && parentAppliance.canUse && playerHold && !playerHold.itemLock)
            {
                globalClicked.applianceInteract = true;
                parentObject.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.AllBuffered, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                                    player.GetComponent<PhotonView>().ViewID);
            }
                
        }
    }
}
