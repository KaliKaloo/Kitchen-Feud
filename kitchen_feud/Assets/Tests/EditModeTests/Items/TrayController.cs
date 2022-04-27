using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class TrayControllerTests
{
    TrayController trayController;
    GameObject tray1;
    GameObject tray2;
    GameObject tray3;


    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        tray1 = new GameObject();
        tray1.AddComponent<Tray>();
        tray1.GetComponent<Tray>().tray = ScriptableObject.CreateInstance<TraySO>();
        tray2 = new GameObject();
        tray2.AddComponent<Tray>();
        tray2.GetComponent<Tray>().tray = ScriptableObject.CreateInstance<TraySO>();
        tray3 = new GameObject();
        tray3.AddComponent<Tray>();
        tray3.GetComponent<Tray>().tray = ScriptableObject.CreateInstance<TraySO>();
    }


    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        trayController = obj.AddComponent<TrayController>();
    }

    [Test]
    public void make1TrayEmpty()
    {
        tray1.GetComponent<Tray>().tray.trayID = "";
        trayController.trays.Add(tray1);
        trayController.makeTray("order 1");
        Assert.AreEqual("order 1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
    }

    [Test]
    public void make1TrayNotEmpty()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
        trayController.makeTray("order 1");
        Assert.AreEqual("tray1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
    }


    [Test]
    public void make3TrayAllEmpty()
    {
        tray1.GetComponent<Tray>().tray.trayID = "";
        tray2.GetComponent<Tray>().tray.trayID = "";
        tray3.GetComponent<Tray>().tray.trayID = "";

        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
        trayController.makeTray("order 1");
        Assert.AreEqual("order 1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);

    }

    [Test]
    public void make3TrayAllNotEmpty()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";

        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
        trayController.makeTray("order 1");
        Assert.AreEqual("tray1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("tray2", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("tray3", trayController.trays[2].GetComponent<Tray>().tray.trayID);

    }

    [Test]
    public void make3TrayMixed()
    {
        tray1.GetComponent<Tray>().tray.trayID = "";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "";

        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
        trayController.makeTray("order 1");
        Assert.AreEqual("order 1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("tray2", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
    }


    [Test]
    public void make3Tray2ndEmpty()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "";
        tray3.GetComponent<Tray>().tray.trayID = "";

        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
        trayController.makeTray("order 1");
        Assert.AreEqual("tray1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("order 1", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
    }


    [Test]
    public void make3Traymultiple()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "";
        tray3.GetComponent<Tray>().tray.trayID = "";
        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
        trayController.makeTray("order 1");
        trayController.makeTray("order 2");
        Assert.AreEqual("tray1", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("order 1", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("order 2", trayController.trays[2].GetComponent<Tray>().tray.trayID);

    }


    [Test]
    public void resetOneTray()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        trayController.trays.Add(tray1);
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual(0, trayController.trays[0].GetComponent<Tray>().tray.ServingTray.Count);
        Assert.AreEqual(0, trayController.trays[0].GetComponent<Tray>().tray.objectsOnTray.Count);

    }




/*    [Test]
    public void resetAllTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";
        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[1].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[2].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
    }*/



/*    [Test]
    public void resetSomeTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";
        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        trayController.resetTray(trayController.trays[2].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("tray2", trayController.trays[1].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", trayController.trays[2].GetComponent<Tray>().tray.trayID);
    }*/

/*

    [Test]
    public void resetMakeAllTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";
        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
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
*/


/*    [Test]
    public void resetSomeMakeTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "tray2";
        tray3.GetComponent<Tray>().tray.trayID = "tray3";
        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
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
    }*/
/*
    [Test]
    public void makeResetTrays()
    {
        tray1.GetComponent<Tray>().tray.trayID = "tray1";
        tray2.GetComponent<Tray>().tray.trayID = "";
        tray3.GetComponent<Tray>().tray.trayID = "";
        trayController.trays.Add(tray1);
        trayController.trays.Add(tray2);
        trayController.trays.Add(tray3);
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
*/

    /*[Test]
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
        trayController.trays.Add(tray1);
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("", grandChild.GetComponentInChildren<TextMeshProUGUI>().text);
    }
*/
    /*
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
        trayController.trays.Add(tray1);
        trayController.resetTray(trayController.trays[0].GetComponent<Tray>());
        Assert.AreEqual("", trayController.trays[0].GetComponent<Tray>().tray.trayID);
        Assert.AreEqual("grandChild Me", grandChild.GetComponentInChildren<TextMeshProUGUI>().text);


    }
*/




}
