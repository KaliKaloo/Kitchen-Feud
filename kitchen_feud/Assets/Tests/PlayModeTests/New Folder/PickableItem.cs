using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.TestTools;
using System.IO;
using TMPro;


public class PickableItemTests : PhotonTestSetup
{
    GameObject obj, chips, potato;
    PlayerHolding playerHold;
    Tray tray;

    GameObject stoveObj;
    Appliance stove;



    [UnitySetUp]
    public IEnumerator Setup()
    {
        chips = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Chips"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
        obj.AddComponent<PhotonPlayer>();
        playerHold = obj.GetComponent<PlayerHolding>();
        
        GameObject trayObj = GameObject.Find("Tray1");
        tray = trayObj.GetComponent<Tray>();
        tray.player = obj.transform;
        stoveObj = GameObject.Find("stove1");
        stove = stoveObj.GetComponent<Appliance>();
        stove.player = obj.transform;

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator MovementTearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);
        yield return null;
    }

  

    [Test]
    public void dropDish()
    {
        pickableItem pick = chips.GetComponent<pickableItem>();
        playerHold.pickUpItem(chips, chips.GetComponent<pickableItem>().item);
        pick.player = obj.transform;

        pick.Interact();
        Assert.IsNull(playerHold.heldObj);
    }


    [Test]
    public void dropIngredient()
    {
        pickableItem pick = potato.GetComponent<pickableItem>();
        playerHold.pickUpItem(potato, potato.GetComponent<pickableItem>().item);
        pick.player = obj.transform;

        pick.Interact();
        Assert.IsNull(playerHold.heldObj);
    }


    [Test]
    public void pickUp()
    {
        pickableItem pick = chips.GetComponent<pickableItem>();
        pick.player = obj.transform;

        pick.Interact();
        Assert.AreEqual(chips, playerHold.heldObj);
        Assert.IsFalse(pick.onTray);
        Assert.IsFalse(pick.onAppliance);

    }


    [Test]
    public void pickDishFromTray()
    {
        pickableItem pick = potato.GetComponent<pickableItem>();
        pick.player = obj.transform;
        pick.onTray = true;
        pick.tray2 = tray;
        pick.Interact();
        Assert.AreEqual(potato, playerHold.heldObj);
        Assert.IsFalse(pick.onTray);

    }


    [Test]
    public void pickIngredientFromTray()
    {
        pickableItem pick = potato.GetComponent<pickableItem>();
        pick.player = obj.transform;
        pick.onTray = true;
        pick.tray2 = tray;
        pick.Interact();
        Assert.AreEqual(potato, playerHold.heldObj);
        Assert.IsFalse(pick.onTray);

    }


    [Test]
    public void pickDishFromAppliance()
    {
        pickableItem pick = potato.GetComponent<pickableItem>();
        pick.player = obj.transform;
        pick.onAppliance = true;
        pick.appliance = stove;
        pick.applianceSlots = stoveObj.GetComponent<SlotsController>();
        pick.Interact();
        Assert.AreEqual(potato, playerHold.heldObj);
        Assert.IsFalse(pick.onAppliance);

    }

}
