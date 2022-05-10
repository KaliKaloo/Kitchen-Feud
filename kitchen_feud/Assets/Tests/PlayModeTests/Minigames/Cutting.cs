using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using System.IO;



public class Cutting: PhotonTestSetup
{
    GameObject obj;
    GameObject lettuce, tomato, cucumber;

    PlayerHolding playerHold;
    Appliance choppingBoard;


    [UnitySetUp]
    public IEnumerator Setup()
    {

        cucumber = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "cucumber 1"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        lettuce = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "lettuce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        tomato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "tomato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);

        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        choppingBoard = GameObject.Find("chopping_board1").GetComponent<Appliance>();
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
        playerHold = obj.GetComponent<PlayerHolding>();
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
    public IEnumerator correctIngredients()
    {
        choppingBoard.player = obj.transform;
        playerHold.pickUpItem(cucumber);
        choppingBoard.Interact();
        playerHold.pickUpItem(lettuce);
        choppingBoard.Interact();
        playerHold.pickUpItem(tomato);
        choppingBoard.Interact();
        choppingBoard.Interact();
        Assert.IsTrue(choppingBoard.minigameCanvas.activeSelf);
        choppingBoard.itemsOnTheAppliance.Clear();
        choppingBoard.isBeingInteractedWith = false;
        choppingBoard.minigameCanvas.SetActive(false);

        yield return null;
    }


    [UnityTest]
    public IEnumerator correctCookedDish()
    {
        choppingBoard.player = obj.transform;
        playerHold.pickUpItem(tomato);
        choppingBoard.Interact();
        playerHold.pickUpItem(lettuce);
        choppingBoard.Interact();
        playerHold.pickUpItem(cucumber);
        choppingBoard.Interact();
        choppingBoard.Interact();
        Assert.AreEqual("Salad(Clone)", choppingBoard.cookedDish.name);
        choppingBoard.minigameCanvas.SetActive(false);
        choppingBoard.itemsOnTheAppliance.Clear();
        choppingBoard.isBeingInteractedWith = false;
        yield return null;

    }


    [UnityTest]
    public IEnumerator sandwichStationIncorrectIngredients()
    {
        choppingBoard.player = obj.transform;
        playerHold.pickUpItem(lettuce);
        choppingBoard.Interact();
        choppingBoard.Interact();
        Assert.IsFalse(choppingBoard.minigameCanvas.activeSelf);
        choppingBoard.itemsOnTheAppliance.Clear();
        choppingBoard.isBeingInteractedWith = false;
        yield return null;

    }


    [UnityTest]
    public IEnumerator startMG()
    {
        choppingBoard.player = obj.transform;
        playerHold.pickUpItem(cucumber);
        choppingBoard.Interact();
        playerHold.pickUpItem(tomato);
        choppingBoard.Interact();
        playerHold.pickUpItem(lettuce);
        choppingBoard.Interact();

        if (playerHold.slot.childCount >= 1)
            playerHold.dropItem();
        choppingBoard.Interact();
        Assert.IsTrue(choppingBoard.minigameCanvas.activeSelf);
        choppingBoard.minigameCanvas.GetComponentInChildren<cutController>().StartGame();
        Assert.IsFalse(choppingBoard.minigameCanvas.GetComponentInChildren<cutController>().StartButton.activeSelf);
        choppingBoard.itemsOnTheAppliance.Clear();
        choppingBoard.isBeingInteractedWith = false;
        choppingBoard.minigameCanvas.SetActive(false);
        yield return null;
    }


    [UnityTest]
    public IEnumerator exitMG()
    {

        choppingBoard.player = obj.transform;
        playerHold.pickUpItem(lettuce);
        choppingBoard.Interact();
        playerHold.pickUpItem(tomato);
        choppingBoard.Interact();
        playerHold.pickUpItem(cucumber);
        choppingBoard.Interact();
        choppingBoard.Interact();

        if (playerHold.slot.childCount >= 1)
            playerHold.dropItem();
        Assert.IsTrue(choppingBoard.minigameCanvas.activeSelf);
        choppingBoard.minigameCanvas.GetComponentInChildren<cutController>().StartGame();
        yield return new WaitForSeconds(0.5f);
        Assert.IsFalse(choppingBoard.minigameCanvas.GetComponentInChildren<cutController>().StartButton.activeSelf);
        yield return new WaitForSeconds(0.2f);
        choppingBoard.minigameCanvas.GetComponentInChildren<cutController>().backButton.GetComponentInChildren<ExitCuttingMinigame>().TaskOnClick();
        Assert.IsFalse(choppingBoard.minigameCanvas.activeSelf);

        choppingBoard.itemsOnTheAppliance.Clear();
        choppingBoard.isBeingInteractedWith = false;
        choppingBoard.minigameCanvas.SetActive(false);


        yield return null;

    }
}

