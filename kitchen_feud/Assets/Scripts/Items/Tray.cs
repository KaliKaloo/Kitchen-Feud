using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Photon.Pun;

public class Tray : Interactable
{
    public TraySO tray;
    public BaseFood item;
    public GameObject objectHolding;
    public List<Transform> slots = new List<Transform>();
    public GameObject currentPrefab;
    public GameObject Agent;
    PlayerHolding playerHold;
    public GameObject SP;
    public pickableItem pickable;
    public GameObject teamController;
    private CanvasController canvasController;
    public bool isReady;
    public PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        isReady = false;
        canvasController = teamController.GetComponent<CanvasController>();
    }

    public override void Interact()
    {
        playerHold = player.GetComponent<PlayerHolding>();
        objectHolding = playerHold.heldObj;

        if (player.transform.Find("slot").childCount == 1)
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
                pickable.GetComponent<PhotonView>().RPC("trayBool", RpcTarget.All, pickable.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID);
                if (objectHolding && player.GetComponent<PhotonView>().IsMine)
                {
                    this.GetComponent<PhotonView>().RPC("addComps", RpcTarget.All, this.GetComponent<PhotonView>().ViewID, objectHolding.GetComponent<PhotonView>().ViewID);
                }
            }
        }
        else if (player.transform.Find("slot").childCount == 0 && tray.trayID != "")
        {
            canvasController.TrayOrderOptions(tray.name);
        }
    }

    public void findDestination(int trayID)
    {
        foreach (GameObject s in GameObject.FindGameObjectsWithTag("ServingPoint1"))
        {
            if (s.GetComponent<Serving>().used == false)
            {
                PV.RPC("setDest",RpcTarget.All,trayID,s.GetPhotonView().ViewID);
                s.GetComponent<PhotonView>().RPC("setUsed",RpcTarget.All,s.GetPhotonView().ViewID);
                break;
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
    [PunRPC]
    void setIsReady(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<Tray>().isReady = true;
    }
    [PunRPC]
    void setIsReadyF(int viewID)
    {
        Debug.Log("SetToFalse1");
        PhotonView.Find(viewID).GetComponent<Tray>().isReady = false;
    }
    [PunRPC]
    void setAgent(int trayID, int AgentID)
    {
        PhotonView.Find(trayID).GetComponent<Tray>().Agent = PhotonView.Find(AgentID).gameObject;
    }
    [PunRPC]
    void setAgentF(int trayID)
    {
        PhotonView.Find(trayID).GetComponent<Tray>().Agent = null;
    }

    [PunRPC]
    void setDest(int viewID,int destID)
    {
        PhotonView.Find(viewID).GetComponent<Tray>().SP = PhotonView.Find(destID).gameObject;
    }
    [PunRPC]
    void PlayServingSound(int viewID) {
        PhotonView.Find(viewID).gameObject.GetComponent<AudioSource>().Play();
    }

} 
 