using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Photon.Pun;

public class FireExBox : Interactable
{
    public GameObject slot;
    GameObject FireExt;
    PlayerHolding playerHold;

    public override void Interact()
    {
        playerHold = player.GetComponent<PlayerHolding>();
        if(playerHold.transform.Find("slot").childCount == 1 && player.GetComponent<PhotonView>().IsMine){
            FireExt = playerHold.heldObj;
            if(FireExt.name == "fireExtinguisher"){
                    FireExt.GetComponent<PhotonView>().RPC("putBackFireExt", RpcTarget.AllBuffered, FireExt.GetComponent<PhotonView>().ViewID, slot.GetComponent<PhotonView>().ViewID);
                    FireExt.layer = 0;
            }

        }


    }
}
