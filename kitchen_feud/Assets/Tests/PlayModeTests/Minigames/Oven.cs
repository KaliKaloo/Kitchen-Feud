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
    Appliance oven1, oven2;
    


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
        obj.name = "Local";
        oven1 = GameObject.Find("Oven1").GetComponent<Appliance>();
        oven2 = GameObject.Find("Oven2").GetComponent<Appliance>();

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
    public IEnumerator correctIngredientsOven1()
    {
        oven1.player = obj.transform;
        playerHold.pickUpItem(cake);
        
        oven1.Interact();
        oven1.Interact();
        Assert.IsTrue(oven1.minigameCanvas.activeSelf);
        oven1.itemsOnTheAppliance.Clear();
        oven1.isBeingInteractedWith = false;
        oven1.minigameCanvas.SetActive(false);

        yield return null;
    }

    [UnityTest]
    public IEnumerator correctIngredientsOven2()
    {
        oven2.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven2.Interact();
        oven2.Interact();
        Assert.IsTrue(oven2.minigameCanvas.activeSelf);
        oven2.itemsOnTheAppliance.Clear();
        oven2.isBeingInteractedWith = false;
        oven2.minigameCanvas.SetActive(false);
        yield return null;
    }


    [UnityTest]
    public IEnumerator correctCookedDishOven1()
    {
        oven1.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven1.Interact();
        oven1.Interact();
        Assert.AreEqual("Cake(Clone)", oven1.cookedDish.name);

        oven1.minigameCanvas.SetActive(false);
        oven1.itemsOnTheAppliance.Clear();
        oven1.isBeingInteractedWith = false;
        yield return null;

    }

    [UnityTest]
    public IEnumerator correctCookedDishOven2()
    {
        oven2.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven2.Interact();
        oven2.Interact();
        Assert.AreEqual("Cake(Clone)", oven2.cookedDish.name);
        oven2.minigameCanvas.SetActive(false);
        oven2.itemsOnTheAppliance.Clear();
        oven2.isBeingInteractedWith = false;
        yield return null;

    }


    [UnityTest]
    public IEnumerator canvasAppearsOven1()
    {
        oven1.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven1.Interact();
        oven1.Interact();
        Assert.IsNotNull(GameObject.Find("ovencanvas(Clone)"));
        oven1.minigameCanvas.SetActive(false);
        oven1.itemsOnTheAppliance.Clear();
        oven1.isBeingInteractedWith = false;
        yield return null;

    }

    [UnityTest]
    public IEnumerator canvasAppearsOven2()
    {
        oven2.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven2.Interact();
        oven2.Interact();
        Assert.IsNotNull(GameObject.Find("ovencanvas(Clone)"));
        oven2.minigameCanvas.SetActive(false);
        oven2.itemsOnTheAppliance.Clear();
        oven2.isBeingInteractedWith = false;
        yield return null;

    }



    [UnityTest]
    public IEnumerator incorrectIngredientsOven1()
    {
        oven1.player = obj.transform;
        playerHold.pickUpItem(tomato);
        oven1.Interact();
        oven1.Interact();

        Assert.IsFalse(oven1.minigameCanvas);
        oven1.itemsOnTheAppliance.Clear();
        oven1.isBeingInteractedWith = false;
        yield return null;

    }


    [UnityTest]
    public IEnumerator incorrectIngredientsOven2()
    {
        oven2.player = obj.transform;
        playerHold.pickUpItem(tomato);
        oven2.Interact();
        oven2.Interact();

        Assert.IsFalse(oven2.minigameCanvas);
        oven2.itemsOnTheAppliance.Clear();
        oven2.isBeingInteractedWith = false;
        yield return null;

    }


    

    [UnityTest]
    public IEnumerator exitMGOven1()
    {
        oven1.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven1.Interact();
        oven1.Interact();
        Assert.IsTrue(oven1.minigameCanvas.activeSelf);
        Assert.IsNotNull(GameObject.Find("ovencanvas(Clone)"));
        yield return new WaitForSeconds(0.2f);
        oven1.GetComponent<ovenMiniGame>().backbutton.TaskOnClick();
        yield return new WaitForSeconds(0.2f);
        Assert.IsNull(GameObject.Find("ovencanvas(Clone)"));
        oven1.itemsOnTheAppliance.Clear();
        oven1.isBeingInteractedWith = false;

        yield return null;

    }

    [UnityTest]
    public IEnumerator exitMGOven2()
    {

        oven2.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven2.Interact();
        oven2.Interact();
        Assert.IsTrue(oven2.minigameCanvas.activeSelf);
        Assert.IsNotNull(GameObject.Find("ovencanvas(Clone)"));
        yield return new WaitForSeconds(0.2f);
        oven2.GetComponent<ovenMiniGame>().backbutton.TaskOnClick();
        yield return new WaitForSeconds(0.2f);
        Assert.IsNull(GameObject.Find("ovencanvas(Clone)"));
        oven2.itemsOnTheAppliance.Clear();
        oven2.isBeingInteractedWith = false;

        yield return null;

    }



    [UnityTest]
    public IEnumerator oven1Fire()
    {
        oven1.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven1.Interact();
        oven1.Interact();
        Assert.IsTrue(oven1.minigameCanvas.activeSelf);
        Assert.IsNotNull(GameObject.Find("ovencanvas(Clone)"));
        GameObject.Find("ovencanvas(Clone)").GetComponent<Timer>().ChangeTimerValue(-6);

        ParticleSystem[] PS = GameObject.Find("ovencanvas(Clone)").transform.parent.GetComponentsInChildren<ParticleSystem>();
       
        foreach(ParticleSystem p in PS){
            if(p.GetComponent<FireOut>()){
                FireOut fireOut = p.GetComponent<FireOut>();
                Assert.IsTrue(fireOut.enabled);

                fireOut.enabled=false;
            }
            p.Stop();
        }

        yield return new WaitForSeconds(0.2f);
        oven1.GetComponent<ovenMiniGame>().backbutton.TaskOnClick();
        yield return new WaitForSeconds(0.2f);
        Assert.IsNull(GameObject.Find("ovencanvas(Clone)"));
        oven1.itemsOnTheAppliance.Clear();
        oven1.isBeingInteractedWith = false;
    }

    
    [UnityTest]
    public IEnumerator oven2Fire()
    {
        oven2.player = obj.transform;
        playerHold.pickUpItem(cake);
        oven2.Interact();
        oven2.Interact();
        Assert.IsTrue(oven2.minigameCanvas.activeSelf);
        Assert.IsNotNull(GameObject.Find("ovencanvas(Clone)"));
        GameObject.Find("ovencanvas(Clone)").GetComponent<Timer>().ChangeTimerValue(-6);
        ParticleSystem[] PS = GameObject.Find("ovencanvas(Clone)").transform.parent.GetComponentsInChildren<ParticleSystem>();
       
        foreach(ParticleSystem p in PS){
            if(p.GetComponent<FireOut>()){
                FireOut fireOut = p.GetComponent<FireOut>();
                Assert.IsTrue(fireOut.enabled);

                fireOut.enabled=false;
            }
            p.Stop();
        }

        yield return new WaitForSeconds(0.2f);
        oven2.GetComponent<ovenMiniGame>().backbutton.TaskOnClick();
        yield return new WaitForSeconds(0.2f);
        Assert.IsNull(GameObject.Find("ovencanvas(Clone)"));
        oven2.itemsOnTheAppliance.Clear();
        oven2.isBeingInteractedWith = false;
    }



}
