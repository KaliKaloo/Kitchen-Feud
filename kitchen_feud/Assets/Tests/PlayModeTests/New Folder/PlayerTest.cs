using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.TestTools;
using System.IO;

public class PlayerTests : PhotonTestSetup
{
    GameObject obj;
    GameObject mushroom, potato;

    PlayerHolding playerHold;
    Appliance stove;


    [UnitySetUp]
    public IEnumerator Setup()
    {
        
        mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        stove = GameObject.Find("stove1").GetComponent<Appliance>();
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
    public IEnumerator CheckMovement()
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        Vector3 pos = rb.position;
        Vector3 movementForward = obj.transform.forward * 1;
        Vector3 movementBackward = obj.transform.forward * -1;
        Vector3 movementRight = obj.transform.right * 1;
        Vector3 movementLeft = obj.transform.right * -1;
        rb.MovePosition(rb.position + movementForward * 1 * Time.fixedDeltaTime);
        Assert.AreNotSame(rb.position, pos);


        pos = rb.position;
        rb.MovePosition(rb.position + movementBackward * 1 * Time.fixedDeltaTime);
        Assert.AreNotSame(rb.position, pos);


        pos = rb.position;
        rb.MovePosition(rb.position + movementRight * 1 * Time.fixedDeltaTime);
        Assert.AreNotSame(rb.position.magnitude, pos.magnitude);

        pos = rb.position;
        rb.MovePosition(rb.position + movementLeft * 1 * Time.fixedDeltaTime);
        Assert.AreNotSame(rb.position.magnitude, pos.magnitude);
        yield return null;
    }

    [UnityTest]
    public IEnumerator pickUp()
    {
        playerHold.pickUpItem(mushroom);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        Assert.IsTrue(obj.transform.GetChild(2).childCount > 0);
        Assert.IsTrue(obj.transform.GetChild(2).GetChild(0).name == mushroom.name);
        playerHold.dropItem();
        yield return null;
    }

    [UnityTest]
    public IEnumerator dropItem()
    {
        playerHold.pickUpItem(mushroom);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        playerHold.dropItem();
        Assert.IsNull(playerHold.heldObj);
        Assert.IsTrue(obj.transform.GetChild(2).childCount == 0);
        yield return null;
    }


    [UnityTest]
    public IEnumerator ApplianceSlot()
    {
        stove.player = obj.transform;
        stove.addItem(mushroom, playerHold);
        Assert.IsTrue(stove.itemsOnTheAppliance.Count == 1);
        yield return null;
    }


    [UnityTest]
    public IEnumerator ApplianceCookTest()
    {
        playerHold.pickUpItem(mushroom);
        stove.player = obj.transform;
        stove.Interact();
        playerHold.pickUpItem(potato);
        stove.Interact();
        stove.Interact();
        Assert.IsTrue(stove.cookedDish.name.Contains("Mushroom Soup"));
        stove.player = null;
        yield return null;
    }


}