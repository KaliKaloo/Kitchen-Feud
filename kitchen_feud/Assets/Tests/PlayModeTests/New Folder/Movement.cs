using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using UnityEngine.TestTools;
using System.IO;

public class Movement : PhotonTestSetup
{
    GameObject obj;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "cat_playerModel"),
            new Vector3(0, 0, 0),
            Quaternion.identity,
            0
        );
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
    public IEnumerator CheckName()
    {
        //Assert.AreEqual("", h.getHealth());
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        Debug.Log(obj.name);

        yield return null;
    }
}
