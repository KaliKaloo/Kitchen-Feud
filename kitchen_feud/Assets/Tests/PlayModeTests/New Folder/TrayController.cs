using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.IO;


public class TrayControllerTests : PhotonTestSetup
{
    GameObject obj;
    Tray tray1, tray2, tray3;

    TrayController trayController;

    
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
        GameObject trayObj = GameObject.Find("Tray1");
        tray1 = trayObj.GetComponent<Tray>();
        trayObj = GameObject.Find("Tray2");
        tray2 = trayObj.GetComponent<Tray>();
        trayObj = GameObject.Find("Tray3");
        tray3 = trayObj.GetComponent<Tray>();
        trayController = GameObject.Find("Team1OrderController").GetComponent<TrayController>();
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);
        yield return null;
    }


    [Test]
    public void resetOneTray()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        trayController.resetTray(tray1);
        Assert.AreEqual("", tray1.GetComponent<Tray>().tray.trayID);
        Assert.AreEqual(0, tray1.GetComponent<Tray>().tray.ServingTray.Count);
        Assert.AreEqual(0, tray1.GetComponent<Tray>().tray.objectsOnTray.Count);

    }


   [Test]
    public void resetAllTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[1].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[2].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
    }



    [Test]
    public void resetSomeTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[2].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("tray2", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
    }


    [Test]
    public void resetMakeAllTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";
       
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[1].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[2].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
        trayController.makeTray("order 1");
        Assert.AreEqual("order 1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
    }


    [Test]
    public void resetSomeMakeTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";
        trayController.resetTray(trayController.trays[1].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[2].GetComponent<Tray>());
        Assert.AreEqual("tray1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
        trayController.makeTray("order 1");
        trayController.makeTray("order 2");
        Assert.AreEqual("tray1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("order 1", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("order 2", trayController.trays[2].GetComponent<Tray>().tray.trayID);
    }

    [Test]
    public void makeResetTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "";
        tray3.GetComponent<Tray>().tray.trayID = "";
        trayController.makeTray("order 1");
        trayController.makeTray("order 2");
        Assert.AreEqual("tray1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("order 1", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("order 2", trayController.trays[2].GetComponent<Tray>().tray.trayID);
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[2].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("order 1", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
       
    }

    [Test]
    public void resetOrderTower()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        GameObject child = new GameObject();
        child.tag = "OrderTower";
        child.transform.SetParent(tray1.transform);
        GameObject grandChild = new GameObject();
        grandChild.AddComponent<TextMeshProUGUI>();
        grandChild.transform.SetParent(child.transform);
        grandChild.GetComponentInChildren<TextMeshProUGUI>().text ="grandChild Me";
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", grandChild.GetComponentInChildren<TextMeshProUGUI>().text);
    }


    [Test]
    public void resetNotOrderTower()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        GameObject child = new GameObject();
        child.tag = "ItemCollider";
        child.transform.SetParent(tray1.transform);
        GameObject grandChild = new GameObject();
        grandChild.AddComponent<TextMeshProUGUI>();
        grandChild.transform.SetParent(child.transform);
        grandChild.GetComponentInChildren<TextMeshProUGUI>().text ="grandChild Me";
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("grandChild Me", grandChild.GetComponentInChildren<TextMeshProUGUI>().text);

    }


}
