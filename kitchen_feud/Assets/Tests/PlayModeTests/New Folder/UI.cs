
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.TestTools;
using System.IO;

public class UI : PhotonTestSetup
{
    GameObject obj;
    GameObject Team1;
    GameObject Team2;

   

    [UnitySetUp]
    public IEnumerator Setup()
    {
        
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "cat_playerModel"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        Team1 = GameObject.FindGameObjectWithTag("Team1");
        Team2 = GameObject.FindGameObjectWithTag("Team2");
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
        obj.AddComponent<PhotonPlayer>();

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
    public IEnumerator checkTeam1Ticket()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 1;
        yield return new WaitForSeconds(0.1f);
        
        Assert.IsTrue(Team1.activeSelf);
        Assert.IsFalse(Team2.activeSelf);

    }


    [UnityTest]
    public IEnumerator checkTeam2Ticket()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 2;
        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(Team1.activeSelf);
        Debug.Log(Team2);
        Assert.IsTrue(Team2.activeSelf);

    }

}