
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
    GameObject mushroom;
    PlayerHolding playerHold;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        
        mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "cat_playerModel"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        playerHold = obj.GetComponent<PlayerHolding>();
        //h = obj.GetComponent<EnemyHealth>();

        //h.maxHealth = 100;
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator MovementTearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);
        yield return null;
    }

    //// A Test behaves as an ordinary method
    //[Test]
    //public void MovementSimplePasses()
    //{
    //    // Use the Assert class to test conditions
    //}

    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CheckMovement()
    {
        //Assert.AreEqual("", h.getHealth());
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
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
        playerHold.pickUpItem(mushroom, mushroom.GetComponent<IngredientItem>().item);
        Assert.IsTrue(obj.transform.GetChild(2).childCount > 0);
        Assert.IsTrue(obj.transform.GetChild(2).GetChild(0).name == mushroom.name);


        yield return null;
    }
    public IEnumerator dropItem()
    {
        playerHold.dropItem();

        Assert.IsTrue(obj.transform.GetChild(2).childCount == 0);
        

        yield return null;
    }

}