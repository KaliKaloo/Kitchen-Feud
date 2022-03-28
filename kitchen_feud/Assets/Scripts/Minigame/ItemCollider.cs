using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemCollider : MonoBehaviour
{
    private GameObject parentObject;
    private Appliance parentAppliance;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = gameObject.transform.parent.gameObject;
        parentAppliance = parentObject.GetComponent<Appliance>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ingredient"))
        {
            // Get player
            GameObject player = collision.transform.parent.parent.gameObject;
            PhotonView pv = player.GetComponent<PhotonView>();
            // Send player to appliance
            parentAppliance.GetComponent<PhotonView>().RPC("setPlayer", RpcTarget.All, parentAppliance.GetComponent<PhotonView>().ViewID,
                pv.ViewID);
            //parentAppliance.player = player.transform;

            PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
            parentAppliance.playerRigidbody = player.GetComponent<Rigidbody>();
            parentAppliance.SlotsController = parentObject.GetComponent<SlotsController>();

            

            if (pv.IsMine && parentAppliance.canUse)
            {
                // PLAY SOUND FOR SLOT HERE
                parentObject.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.All, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                                    player.GetComponent<PhotonView>().ViewID);
                playerHold.GetComponent<PhotonView>().RPC("setHeldobjAsNull", RpcTarget.All, playerHold.GetComponent<PhotonView>().ViewID);
            }
                
        }
    }

    [PunRPC]
    void addItemRPC(int viewID, int viewID1)
    {
        parentAppliance.addItem(PhotonView.Find(viewID).gameObject, PhotonView.Find(viewID1).gameObject.GetComponent<PlayerHolding>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
