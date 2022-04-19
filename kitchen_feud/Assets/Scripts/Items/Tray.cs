using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Tray : Interactable
{
    public TraySO tray;
    public BaseFood item;
    public GameObject objectHolding;
    public List<Transform> slots = new List<Transform>();

    public GameObject currentPrefab;

    PlayerHolding playerHold;
    public pickableItem pickable;
    public GameObject teamController;
    private CanvasController canvasController;

    private void Start()
    {
        canvasController = teamController.GetComponent<CanvasController>();
    }

    public override void Interact()
    {

        playerHold = player.GetComponent<PlayerHolding>();
        objectHolding = playerHold.heldObj;

        if (player.transform.Find("slot").childCount == 1&&
                playerHold.GetComponent<PhotonView>().IsMine)
        {
            //add object holding to tray slot if tray slot empty
            if (tray.ServingTray.Count < 4)
            {
                //foreach (Transform slot in slots)
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].transform.childCount == 0)
                    {

                        objectHolding.GetComponent<PhotonView>().RPC("setParent", RpcTarget.AllBuffered,
                        objectHolding.GetComponent<PhotonView>().ViewID, slots[i].GetComponent<PhotonView>().ViewID);
                        pickable = objectHolding.GetComponent<pickableItem>();
                
                        pickable.GetComponent<PhotonView>().RPC("trayBool", RpcTarget.AllBuffered, pickable.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID);
                        objectHolding.layer = 0;
                        foreach ( Transform child in objectHolding.transform )
                        {
                            child.gameObject.layer = 0;
                        }
                        if (objectHolding && player.GetComponent<PhotonView>().IsMine)
                        {
                            this.GetComponent<PhotonView>().RPC("addComps", RpcTarget.AllBuffered, this.GetComponent<PhotonView>().ViewID, objectHolding.GetComponent<PhotonView>().ViewID);
                        }
                        break;
                    }
                }
                
            }
        }
        else if (player.transform.Find("slot").childCount == 0 && tray.trayID != "")
        {
            globalClicked.trayInteract = true;
            canvasController.TrayOrderOptions(tray.name);
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

    [PunRPC]
    void PlayServingSound(int viewID) {
        PhotonView.Find(viewID).gameObject.GetComponent<AudioSource>().Play();
    }

} 
 