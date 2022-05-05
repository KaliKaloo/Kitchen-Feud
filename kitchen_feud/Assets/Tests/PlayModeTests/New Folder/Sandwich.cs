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

public class SandwichTests : PhotonTestSetup
{
    GameObject obj;
    GameObject bread, PB, jam, lettuce;

    PlayerHolding playerHold;
    Appliance sandwichStation;


    [UnitySetUp]
    public IEnumerator Setup()
    {
        
        bread = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        PB = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "peanutButter"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        jam = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "jam"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        lettuce = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "lettuce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        sandwichStation = GameObject.Find("sandwhich_station1").GetComponent<Appliance>();
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
        sandwichStation.player = obj.transform;
        playerHold.pickUpItem(bread);
        sandwichStation.Interact();
        playerHold.pickUpItem(PB);
        sandwichStation.Interact();
        playerHold.pickUpItem(jam);
        sandwichStation.Interact();
        sandwichStation.Interact();
        Assert.IsTrue(sandwichStation.minigameCanvas.activeSelf);
        sandwichStation.itemsOnTheAppliance.Clear();
        sandwichStation.isBeingInteractedWith = false;
        sandwichStation.minigameCanvas.SetActive(false);

        yield return null;
    }


    [UnityTest]
    public IEnumerator correctCookedDish()
    {
        sandwichStation.player = obj.transform;
        playerHold.pickUpItem(bread);
        sandwichStation.Interact();
        playerHold.pickUpItem(PB);
        sandwichStation.Interact();
        playerHold.pickUpItem(jam);
        sandwichStation.Interact();
        sandwichStation.Interact();
        Assert.AreEqual("PBJ_sandwich(Clone)", sandwichStation.cookedDish.name);
        sandwichStation.minigameCanvas.SetActive(false);
        sandwichStation.itemsOnTheAppliance.Clear();
        sandwichStation.isBeingInteractedWith = false;
        yield return null;

    }


    [UnityTest]
    public IEnumerator sandwichStationIncorrectIngredients()
    {
        sandwichStation.player = obj.transform;
        playerHold.pickUpItem(lettuce);
        sandwichStation.Interact();
        sandwichStation.Interact();
        Assert.IsFalse(sandwichStation.minigameCanvas.activeSelf);
        sandwichStation.itemsOnTheAppliance.Clear();
        sandwichStation.isBeingInteractedWith = false;
        yield return null;

    }

    
    [UnityTest]
    public IEnumerator startMG()
    {
        sandwichStation.player = obj.transform;
        playerHold.pickUpItem(bread);
        sandwichStation.Interact();
        playerHold.pickUpItem(PB);
        sandwichStation.Interact();
        playerHold.pickUpItem(jam);
        sandwichStation.Interact();
        
        if (playerHold.slot.childCount >= 1)
            playerHold.dropItem();
        sandwichStation.Interact();
        Assert.IsTrue(sandwichStation.minigameCanvas.activeSelf);
        sandwichStation.minigameCanvas.GetComponentInChildren<SandwichController>().StartGame();
        Assert.IsFalse(sandwichStation.minigameCanvas.GetComponentInChildren<SandwichController>().StartButton.activeSelf);
        sandwichStation.itemsOnTheAppliance.Clear();
        sandwichStation.isBeingInteractedWith = false;
        sandwichStation.minigameCanvas.SetActive(false);
        yield return null;
    }


    [UnityTest]
    public IEnumerator exitMG()
    {

        sandwichStation.player = obj.transform;
        playerHold.pickUpItem(bread);
        sandwichStation.Interact();
        playerHold.pickUpItem(PB);
        sandwichStation.Interact();
        playerHold.pickUpItem(jam);
        sandwichStation.Interact();
        sandwichStation.Interact();

        if (playerHold.slot.childCount >= 1)
            playerHold.dropItem();
        sandwichStation.Interact();
        Assert.IsTrue(sandwichStation.minigameCanvas.activeSelf);
        sandwichStation.minigameCanvas.GetComponentInChildren<SandwichController>().StartGame();
        Assert.IsFalse(sandwichStation.minigameCanvas.GetComponentInChildren<SandwichController>().StartButton.activeSelf);
     
        sandwichStation.minigameCanvas.GetComponentInChildren<SandwichController>().backButton.GetComponent<ExitSandwichMinigame>().TaskOnClick();
        yield return new WaitForSeconds(0.2f);
        Assert.IsFalse(sandwichStation.minigameCanvas.activeSelf);

        sandwichStation.itemsOnTheAppliance.Clear();
        sandwichStation.isBeingInteractedWith = false;
        sandwichStation.minigameCanvas.SetActive(false);



      
      
        yield return null;

    }


}
