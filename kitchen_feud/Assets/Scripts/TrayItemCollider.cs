using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrayItemCollider : MonoBehaviour
{
    private GameObject parentObject;
    private TraySO tray;
    private List<Transform> slots = new List<Transform>();
    private pickableItem pickable;


    // Start is called before the first frame update
    void Start()
    {
        parentObject = gameObject.transform.parent.gameObject;
        tray = parentObject.GetComponent<Tray>().tray;
        slots = parentObject.GetComponent<Tray>().slots;
    }

    void OnTriggerEnter(Collider collision)
    {
        GameObject player = collision.transform.parent.gameObject.transform.parent.gameObject;
        if (collision.CompareTag("Ingredient") && player.name == "Local")
        {
            // Get player
            PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
            // If item is not locked from picking up then proceed to slot
            if (!playerHold.itemLock)
            {
                GameObject objectHolding = playerHold.heldObj;


                if (playerHold.items.Count == 1)
                {
                    //add object holding to tray slot if tray slot empty
                    if (tray.ServingTray.Count < 4)
                    {
                        //foreach (Transform slot in slots)
                        for (int i = 0; i < slots.Count; i++)
                        {
                            if (slots[i].transform.childCount == 0)
                            {
                                playerHold.dropItem();

                                objectHolding.GetComponent<PhotonView>().RPC("setParent", RpcTarget.All,
                                objectHolding.GetComponent<PhotonView>().ViewID, slots[i].GetComponent<PhotonView>().ViewID);
                                break;
                            }
                        }
                        pickable = objectHolding.GetComponent<pickableItem>();
                        pickable.GetComponent<PhotonView>().RPC("trayBool", RpcTarget.All, pickable.GetComponent<PhotonView>().ViewID, parentObject.GetComponent<PhotonView>().ViewID);
                        if (objectHolding && player.GetComponent<PhotonView>().IsMine)
                        {
                            parentObject.GetComponent<PhotonView>().RPC("addComps", RpcTarget.All, parentObject.GetComponent<PhotonView>().ViewID, objectHolding.GetComponent<PhotonView>().ViewID);
                        }
                    }
                }
            }
        }

    }

    [PunRPC]
    void addComps(int viewID, int objID)
    {

        PhotonView.Find(viewID).GetComponent<Tray>().tray.ServingTray.Add(PhotonView.Find(objID).GetComponent<pickableItem>().item);
        PhotonView.Find(viewID).GetComponent<Tray>().tray.objectsOnTray.Add(PhotonView.Find(objID).gameObject);

    }
    [PunRPC]
    void removeFromTray(int viewID)
    {
        this.tray.ServingTray.Remove(PhotonView.Find(viewID).GetComponent<pickableItem>().item);
        this.tray.objectsOnTray.Remove(PhotonView.Find(viewID).gameObject);
    }
}
