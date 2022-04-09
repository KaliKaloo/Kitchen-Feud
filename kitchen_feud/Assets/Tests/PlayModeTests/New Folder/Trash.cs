
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.TestTools;
using System.IO;

public class TrashTest : PhotonTestSetup
{
    GameObject obj;
    GameObject mushroom;
    GameObject chips;


    Trash trash;

    PlayerHolding playerHold;


    
    [UnitySetUp]
    public IEnumerator Setup()
    {
        mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        chips = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Chips"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);

        
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "cat_playerModel"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        playerHold = obj.GetComponent<PlayerHolding>();
        GameObject trashObj = new GameObject();
        trash = trashObj.AddComponent<Trash>();
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;


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
    public IEnumerator trashIngredient()
    {
        playerHold.pickUpItem(mushroom, mushroom.GetComponent<IngredientItem>().item);
        Assert.AreEqual(mushroom, playerHold.heldObj);
        trash.player = obj.transform;
        trash.Interact();
        Assert.IsNull(playerHold.heldObj);
        yield return new WaitForEndOfFrame();
        Assert.IsTrue(mushroom == null);
    }

    [UnityTest]
    public IEnumerator trashDish()
    {
        playerHold.pickUpItem(chips, chips.GetComponent<pickableItem>().item);
        Assert.AreEqual(chips, playerHold.heldObj);
        trash.player = obj.transform;
        trash.Interact();
        Assert.IsNull(playerHold.heldObj);
        yield return new WaitForEndOfFrame();
        Assert.IsTrue(chips == null);
    }

}