using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;


public class TrashTest
{

    Trash trash;
    
    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        trash = obj.AddComponent<Trash>();
        GameObject transObj = new GameObject();
        trash.player = transObj.transform;
    }


    [Test]
    public void TrashHeldObjRemainsNull()
    {
        trash.player.gameObject.AddComponent<PlayerHolding>();
        GameObject heldObj = trash.player.GetComponent<PlayerHolding>().heldObj;
        Assert.IsNull(heldObj);
        trash.Interact();
        Assert.IsNull(heldObj);
    }

    [Test]
    public void TrashHeldObjSetToNull()
    {
        trash.player.gameObject.AddComponent<PlayerHolding>();
        GameObject trashObj = new GameObject();
        trashObj.AddComponent<PhotonView>();
        trash.player.GetComponent<PlayerHolding>().heldObj = trashObj;
        Assert.IsNotNull(trash.player.GetComponent<PlayerHolding>().heldObj);
        trash.Interact();
        Assert.IsNull(trash.player.GetComponent<PlayerHolding>().heldObj);
    }

    [Test]
    public void TrashHoldNull()
    {
        trash.Interact();
    }

}