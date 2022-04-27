using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.TestTools;
using System.IO;
public class TrayTests : PhotonTestSetup
{
    GameObject obj;
    GameObject mushroom, potato, chips, cake, pancakes;
    PlayerHolding playerHold;
    Tray tray;

    
    [UnitySetUp]
    public IEnumerator Setup()
    {
        
        mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        chips = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Chips"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        cake = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Cake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        pancakes = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pancakes"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
        playerHold = obj.GetComponent<PlayerHolding>();
        GameObject trayObj = GameObject.Find("Tray1");
        tray = trayObj.GetComponent<Tray>();
        tray.player = obj.transform;
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator MovementTearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);
        yield return null;
    }

    [UnityTest]
    public IEnumerator setParent()
    {
        playerHold.pickUpItem(mushroom);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        tray.Interact();
        Assert.AreEqual(tray.slots[0].transform, playerHold.heldObj.transform.parent);
        Assert.AreEqual(Vector3.zero, playerHold.heldObj.transform.localPosition);
        tray.slots.Remove(mushroom.transform);
        mushroom.transform.parent = null;
        tray.tray.ServingTray.Clear();
        yield return null;
    }

    [UnityTest]
    public IEnumerator trayBool()
    {
        playerHold.pickUpItem(mushroom);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        tray.Interact();
        Assert.IsTrue(playerHold.heldObj.GetComponent<pickableItem>().onTray);
        Assert.AreEqual(playerHold.heldObj.GetComponent<pickableItem>().Tray, tray.GetComponent<Tray>().tray);
        tray.slots.Remove(mushroom.transform);
        mushroom.transform.parent = null;
        tray.tray.ServingTray.Clear();
        yield return null;
    }

    [UnityTest]
    public IEnumerator layerSetToZero()
    {
        playerHold.pickUpItem(mushroom);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        tray.Interact();
        Assert.AreEqual(0, playerHold.heldObj.layer);
        tray.slots.Remove(mushroom.transform);
        mushroom.transform.parent = null;
        tray.tray.ServingTray.Clear();
        yield return null;
    }

    [UnityTest]
    public IEnumerator oneItemOnTray()
    {
       
        playerHold.pickUpItem(mushroom);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        tray.Interact();
        Assert.IsTrue(playerHold.heldObj.GetComponent<pickableItem>().onTray);
        Assert.IsTrue(playerHold.heldObj.GetComponent<pickableItem>().onTray);
        Assert.AreEqual(1, tray.slots[0].transform.childCount);
        Assert.AreEqual(1, tray.tray.ServingTray.Count);
        tray.slots.Remove(mushroom.transform);
        mushroom.transform.parent = null;
        tray.tray.ServingTray.Clear();
        yield return null;

    }


    [UnityTest]
    public IEnumerator twoItemsOnTray()
    {
        playerHold.pickUpItem(mushroom);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        tray.Interact();
        Assert.AreEqual(1, tray.slots[0].transform.childCount);
        Assert.AreEqual(1, tray.tray.ServingTray.Count);
        playerHold.pickUpItem(potato);
        Assert.AreEqual(potato, playerHold.heldObj);
        tray.Interact();

        Assert.AreEqual(1, tray.slots[0].transform.childCount);
        Assert.AreEqual(1, tray.slots[1].transform.childCount);
        Assert.AreEqual(2, tray.tray.ServingTray.Count);
        tray.slots.Remove(mushroom.transform);
        tray.slots.Remove(potato.transform);
        tray.tray.ServingTray.Clear();

        yield return null;

    }


    [UnityTest]
    public IEnumerator trayFull()
    {
        playerHold.pickUpItem(mushroom);
        tray.Interact();
        playerHold.pickUpItem(potato);
        tray.Interact();
        playerHold.pickUpItem(chips);
        tray.Interact();

        playerHold.pickUpItem(pancakes);
        tray.Interact();
        Assert.AreEqual(4, tray.tray.ServingTray.Count);
        playerHold.pickUpItem(cake);
        tray.Interact();
        Assert.AreEqual(cake, playerHold.heldObj);
        Assert.AreEqual(4, tray.tray.ServingTray.Count);
      
        tray.slots.Remove(mushroom.transform);
        tray.slots.Remove(potato.transform);
        tray.slots.Remove(chips.transform);
        tray.slots.Remove(pancakes.transform);
        mushroom.transform.parent = null;
        potato.transform.parent = null;
        chips.transform.parent = null;
        pancakes.transform.parent = null;
        tray.tray.ServingTray.Clear();

        yield return null;

    }


    [UnityTest]
    public IEnumerator removeItem()
    {
       
        playerHold.pickUpItem(mushroom);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        tray.Interact();
        Assert.AreEqual(1, tray.slots[0].transform.childCount);
        Assert.AreEqual(1, tray.tray.ServingTray.Count);
        mushroom.GetComponent<pickableItem>().player = obj.transform;
        mushroom.GetComponent<pickableItem>().Interact();
        tray.slots.Remove(mushroom.transform);
        Assert.AreEqual(0, tray.slots[0].transform.childCount);
        Assert.AreEqual(0, tray.tray.ServingTray.Count);
        mushroom.transform.parent = null;
        tray.tray.ServingTray.Clear();
        yield return null;

    }

    [UnityTest]
    public IEnumerator serveMenuActive()
    {
        tray.tray.trayID = "trayID";
        Transform t1 = GameObject.Find("Team1").transform.Find("ticket1-1");
        t1.gameObject.SetActive(true);
        tray.Interact();
        Assert.IsTrue(globalClicked.trayInteract);
        Assert.IsTrue(tray.teamController.GetComponent<CanvasController>().orderMenu.activeSelf);
        tray.teamController.GetComponent<CanvasController>().orderMenu.SetActive(false);

        yield return null;
    }

}
