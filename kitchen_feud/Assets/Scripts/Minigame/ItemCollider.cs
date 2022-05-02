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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ingredient") && collision.transform.parent)
     
        {


            // Get playe
            //collision.GetComponent<BoxCollider>().isTrigger = true;
            GameObject player = collision.transform.parent.parent.gameObject;
            PhotonView pv = player.GetComponent<PhotonView>();
            // Send player to appliance
            parentAppliance.GetComponent<PhotonView>().RPC("setPlayer", RpcTarget.AllBuffered, parentAppliance.GetComponent<PhotonView>().ViewID,
                pv.ViewID);
            //parentAppliance.player = player.transform;

            PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
            parentAppliance.playerRigidbody = player.GetComponent<Rigidbody>();
            parentAppliance.SlotsController = parentObject.GetComponent<SlotsController>();

            if (pv.IsMine && parentAppliance.canUse && playerHold && !playerHold.itemLock)
            {
                // PLAY SOUND FOR SLOT HERE
                globalClicked.applianceInteract = true;
                parentObject.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.AllBuffered, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                                    player.GetComponent<PhotonView>().ViewID);
                // playerHold.GetComponent<PhotonView>().RPC("setHeldobjAsNull", RpcTarget.AllBuffered, playerHold.GetComponent<PhotonView>().ViewID);
            }
                
        }
    }

    [PunRPC]
    void addItemRPC(int heldViewID, int viewID1)
    {
        PhotonView.Find(viewID1).GetComponent<PlayerHolding>().heldObj = null;

        parentAppliance.addItem(PhotonView.Find(heldViewID).gameObject, PhotonView.Find(viewID1).gameObject.GetComponent<PlayerHolding>());
    }

}
