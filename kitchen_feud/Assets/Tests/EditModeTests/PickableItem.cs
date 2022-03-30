using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PickableItem
{

    pickableItem item;

    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        item = obj.AddComponent<pickableItem>();

    }

    [Test]
    public void trayInit()
    {
        Assert.IsFalse(item.onTray, "on Tray should be initialised to false");
    }

    
    [Test]
    public void applianceInit()
    {
        Assert.IsFalse(item.onAppliance, "on Appliance not initialised to false");
    }


    [Test]
    public void removeItemFromTray()
    {
        item.item = ScriptableObject.CreateInstance<IngredientSO>();
        TraySO tray = ScriptableObject.CreateInstance<TraySO>();
        tray.ServingTray.Add(item.item);
        Assert.AreEqual(1, tray.ServingTray.Count, "serving tray should have 1 ingredient");

        item.removeFromTray(tray);
        Assert.IsFalse(item.onTray, "on Tray should be set to false");
        Assert.AreEqual(0, tray.ServingTray.Count, "serving tray should be empty");
    }

  
}
