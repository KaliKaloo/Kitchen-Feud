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


    [UnityTest]
    public IEnumerator pickupFireExt()
    {
        fireExtObj.GetComponent<FireExtinguisher>().Interact();
        Assert.AreEqual(fireExtObj, playerHold.heldObj);
        fireExtObj.GetComponent<FireExtinguisher>().Interact();
        yield return null;

    }


    [UnityTest]
    public IEnumerator pickupDropFireExt()
    {   
        fireExtObj.GetComponent<FireExtinguisher>().Interact();
        Assert.AreEqual(fireExtObj, playerHold.heldObj);
        fireExtObj.GetComponent<FireExtinguisher>().Interact();
        Assert.IsNull(playerHold.heldObj);
        yield return null;

    }


    [UnityTest]
    public IEnumerator slotFireExt()
    {
        fireExtObj.GetComponent<FireExtinguisher>().Interact();
        Assert.AreEqual(fireExtObj, playerHold.heldObj);
        fireBox.player = obj.transform;
        fireBox.Interact();
        Assert.AreEqual(1, fireBox.slot.transform.childCount);
        Assert.AreEqual(fireBox.slot.transform, fireExtObj.transform.parent);
        Assert.AreEqual(0, fireExtObj.layer);
        yield return null;

    }




}
