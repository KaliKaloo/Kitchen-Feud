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

public class CanvasControllerTest : PhotonTestSetup
{

    GameObject obj;

    CanvasController contr1;
    CanvasController contr2;



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
        obj.AddComponent<PhotonPlayer>();
        GameObject team1 = GameObject.Find("Team1OrderController");
        contr1 = team1.GetComponent<CanvasController>();
        GameObject team2 = GameObject.Find("Team2OrderController");
        contr2 = team2.GetComponent<CanvasController>();
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator MovementTearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);
        yield return null;
    }



    [Test]
    public void UISetToFalse()
    {
        Assert.IsFalse(contr1.ticket1.activeSelf);
        Assert.IsFalse(contr1.ticket2.activeSelf);
        Assert.IsFalse(contr1.ticket3.activeSelf);
        Assert.IsFalse(contr1.orderMenu.activeSelf);

    }


    [Test]
    public void k1t1TrayOrderOptions()
    {
        contr1.TrayOrderOptions("Tray1-1");
        Assert.IsTrue(contr1.orderMenu.activeSelf);
        Assert.AreEqual(contr1.ticket1, contr1.justClicked);
    }

    [Test]
    public void k1t2TrayOrderOptions()
    {
        contr1.TrayOrderOptions("Tray2-1");
        Assert.IsTrue(contr1.orderMenu.activeSelf);
        Assert.AreEqual(contr1.ticket2, contr1.justClicked);
    }

    [Test]
    public void k1t3TrayOrderOptions()
    {
        contr1.TrayOrderOptions("Tray3-1");
        Assert.IsTrue(contr1.orderMenu.activeSelf);
        Assert.AreEqual(contr1.ticket3, contr1.justClicked);
    }


    [Test]
    public void k2t1TrayOrderOptions()
    {
        contr2.TrayOrderOptions("Tray1-2");
        Assert.IsTrue(contr2.orderMenu.activeSelf);
        Assert.AreEqual(contr2.ticket1, contr2.justClicked);
    }

    [Test]
    public void k2t2TrayOrderOptions()
    {
        contr2.TrayOrderOptions("Tray2-2");
        Assert.IsTrue(contr2.orderMenu.activeSelf);
        Assert.AreEqual(contr2.ticket2, contr2.justClicked);
    }

    [Test]
    public void k2t3TrayOrderOptions()
    {
        contr2.TrayOrderOptions("Tray3-2");
        Assert.IsTrue(contr2.orderMenu.activeSelf);
        Assert.AreEqual(contr2.ticket3, contr2.justClicked);
    }

    [Test]
    public void TaskOnClick(){
        contr1.TaskOnClick();
        Assert.IsFalse(contr1.orderMenu.activeSelf);
        // Assert.AreEqual(contr2.ticket3, contr2.justClicked);
    }
   
   
    
}
