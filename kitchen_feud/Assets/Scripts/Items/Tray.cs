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

        // if (playerHold.items.Count == 0 && tray.trayID != "")
        // if not holding anything and tray has an order then prompt for serve
        if (player.transform.Find("slot").childCount == 1)
        {
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

} 
 