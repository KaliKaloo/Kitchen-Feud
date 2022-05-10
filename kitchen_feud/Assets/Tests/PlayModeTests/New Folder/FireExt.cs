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


public class FireExt : PhotonTestSetup
{   

    GameObject obj, fireExtObj;
    
    FireExBox fireBox;
    PlayerHolding playerHold;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
        playerHold = obj.GetComponent<PlayerHolding>();
        fireExtObj = GameObject.Find("fireExtinguisher");
        fireBox = GameObject.Find("fire_extinguisher_box").GetComponent<FireExBox>();
        fireExtObj.GetComponent<FireExtinguisher>().player = obj.transform;
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);

        yield return null;
    }


    // [OneTimeTearDown]
    // public void OTTearDown()
    // {
    //     if (fireExtObj != null)
    //         PhotonNetwork.Destroy(fireExtObj);
    // }



    [UnityTest]
    public IEnumerator pickupFireExt()
    {
        playerHold.pickUpItem(fireExtObj);
        Assert.AreEqual(fireExtObj, playerHold.heldObj);
        playerHold.dropItem();
        yield return null;

    }


    [UnityTest]
    public IEnumerator pickupDropFireExt()
    {
        playerHold.pickUpItem(fireExtObj);
        Assert.AreEqual(fireExtObj, playerHold.heldObj);
        playerHold.dropItem();
        Assert.IsNull(playerHold.heldObj);
        yield return null;

    }


    [UnityTest]
    public IEnumerator slotFireExt()
    {
        playerHold.pickUpItem(fireExtObj);
        Assert.AreEqual(fireExtObj, playerHold.heldObj);
        fireBox.player = obj.transform;
        fireBox.Interact();
        Assert.AreEqual(1, fireBox.slot.transform.childCount);
        Assert.AreEqual(fireBox.slot.transform, fireExtObj.transform.parent);
        Assert.AreEqual(0, fireExtObj.layer);
        yield return null;

    }




}
