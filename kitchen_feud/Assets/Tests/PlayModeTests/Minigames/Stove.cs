using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.TestTools;
using System.IO;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Stove : PhotonTestSetup
{
    GameObject obj;
    GameObject mushroom, potato, rice;
    Hashtable ht = new Hashtable();
    PlayerHolding playerHold;
    Appliance stove, stove2;


    [UnitySetUp]
    public IEnumerator Setup()
    {
        
        mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        rice = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        stove = GameObject.Find("stove1").GetComponent<Appliance>();
        stove2 = GameObject.Find("stove2").GetComponent<Appliance>();
        ht["ViewID"] = obj.GetPhotonView().ViewID;
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
        playerHold = obj.GetComponent<PlayerHolding>();
        yield return null;
    }


    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ApplianceSlot()
    {
        stove.player = obj.transform;
        playerHold.pickUpItem(mushroom);
        stove.addItem(mushroom, playerHold);
        Assert.IsTrue(stove.itemsOnTheAppliance.Count == 1);
        yield return null;
    }


    [UnityTest]
    public IEnumerator correctIngredients()
    {
        playerHold.pickUpItem(mushroom);
        stove.player = obj.transform;
        stove.Interact();
        playerHold.pickUpItem(potato);
        stove.Interact();
        stove.Interact();
        yield return null;
        Assert.IsTrue(stove.minigameCanvas.activeSelf);
        stove.itemsOnTheAppliance.Clear();
        stove.isBeingInteractedWith = false;
        stove.minigameCanvas.SetActive(false);
        yield return null;
    }

      [UnityTest]
    public IEnumerator correctIngredientsstove2()
    {
        playerHold.pickUpItem(mushroom);
        stove2.player = obj.transform;
        stove2.Interact();
        playerHold.pickUpItem(potato);
        stove2.Interact();
        stove2.Interact();
        Assert.IsTrue(stove2.minigameCanvas.activeSelf);
        stove2.itemsOnTheAppliance.Clear();
        stove2.isBeingInteractedWith = false;
        stove2.minigameCanvas.SetActive(false);
        yield return null;
    }


    [UnityTest]
    public IEnumerator correctCookedDish()
    {
        stove.itemsOnTheAppliance.Clear();
        playerHold.pickUpItem(mushroom);
        stove.player = obj.transform;
        stove.Interact();
        playerHold.pickUpItem(potato);
        stove.Interact();
    
        stove.Interact();
        Debug.Log(stove.itemsOnTheAppliance.Count);


        Assert.AreEqual("Mushroom Soup(Clone)", stove.cookedDish.name);


        stove.minigameCanvas.SetActive(false);
        stove.itemsOnTheAppliance.Clear();
        stove.isBeingInteractedWith = false;
        yield return null;

    }


    [UnityTest]
    public IEnumerator StoveIncorrectIngredients()
    {
        stove.player = obj.transform;
        playerHold.pickUpItem(rice);
        stove.Interact();
        stove.Interact();
        Assert.IsFalse(stove.minigameCanvas.activeSelf);
        stove.itemsOnTheAppliance.Clear();
        stove.isBeingInteractedWith = false;
        yield return null;

    }

    
    [UnityTest]
    public IEnumerator startMG()
    {
        playerHold.pickUpItem(mushroom);
        stove.player = obj.transform;
        stove.Interact();
        playerHold.pickUpItem(potato);
        stove.Interact();
        stove.Interact();
        Assert.IsTrue(stove.minigameCanvas.activeSelf);
        yield return null;
        stove.GetComponent<stoveMinigame>().spawner.StartGame();
        Assert.IsFalse(stove.GetComponent<stoveMinigame>().spawner.startButton.activeSelf);
        stove.itemsOnTheAppliance.Clear();
        stove.isBeingInteractedWith = false;
        stove.minigameCanvas.SetActive(false);
        yield return null;

    }


    [UnityTest]
    public IEnumerator exitMG()
    {
        playerHold.pickUpItem(mushroom);
        stove.player = obj.transform;
        stove.Interact();
        playerHold.pickUpItem(potato);
        stove.Interact();
        stove.Interact();
        Assert.IsTrue(stove.minigameCanvas.activeSelf);
        yield return null;
        stove.GetComponent<stoveMinigame>().spawner.StartGame();
        Assert.IsFalse(stove.GetComponent<stoveMinigame>().spawner.startButton.activeSelf);
        stove.GetComponent<stoveMinigame>().spawner.backButton.GetComponent<ExitStoveMinigame>().TaskOnClick();
        Assert.IsFalse(stove.minigameCanvas.activeSelf);
        stove.itemsOnTheAppliance.Clear();
        stove.isBeingInteractedWith = false;
        stove.minigameCanvas.SetActive(false);
        yield return null;

    }

   
}
