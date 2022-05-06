using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using System.IO;


public class OvenTests : PhotonTestSetup {
   GameObject obj;
    GameObject cake, tomato;

    PlayerHolding playerHold;
    Appliance oven;


    [UnitySetUp]
    public IEnumerator Setup()
    {

        cake = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "unCookedCake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        tomato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "tomato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);

        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        oven = GameObject.Find("Oven1").GetComponent<Appliance>();
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
        oven.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven.Interact();
        oven.Interact();
        Assert.IsTrue(oven.minigameCanvas.activeSelf);
        oven.itemsOnTheAppliance.Clear();
        oven.isBeingInteractedWith = false;
        oven.minigameCanvas.SetActive(false);

        yield return null;
    }


    [UnityTest]
    public IEnumerator correctCookedDish()
    {
        oven.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven.Interact();
        oven.Interact();
        Assert.AreEqual("Cake(Clone)", oven.cookedDish.name);

        oven.minigameCanvas.SetActive(false);
        oven.itemsOnTheAppliance.Clear();
        oven.isBeingInteractedWith = false;
        yield return null;

    }


     [UnityTest]
    public IEnumerator canvasAppears()
    {
        oven.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven.Interact();
        oven.Interact();
        Assert.IsNotNull(GameObject.Find("ovencanvas(Clone)"));
        oven.minigameCanvas.SetActive(false);
        oven.itemsOnTheAppliance.Clear();
        oven.isBeingInteractedWith = false;
        yield return null;

    }


    [UnityTest]
    public IEnumerator ovenIncorrectIngredients()
    {
        oven.player = obj.transform;
        playerHold.pickUpItem(tomato);
        oven.Interact();
        oven.Interact();

        Assert.IsFalse(oven.minigameCanvas);
        oven.itemsOnTheAppliance.Clear();
        oven.isBeingInteractedWith = false;
        yield return null;

    }

    [UnityTest]
    public IEnumerator exitMG()
    {

        oven.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven.Interact();
        oven.Interact();

        Assert.IsTrue(oven.minigameCanvas.activeSelf);
        Assert.IsNotNull(GameObject.Find("ovencanvas(Clone)"));
        
        yield return new WaitForSeconds(0.2f);
        oven.GetComponent<ovenMiniGame>().backbutton.TaskOnClick();

        yield return new WaitForSeconds(0.2f);
        Assert.IsNull(GameObject.Find("ovencanvas(Clone)"));


        oven.itemsOnTheAppliance.Clear();
        oven.isBeingInteractedWith = false;

        yield return null;

    }
}
