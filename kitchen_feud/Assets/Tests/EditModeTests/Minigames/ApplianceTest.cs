using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ApplianceTest
{

    Appliance appliance;

    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        appliance = obj.AddComponent<Appliance>();
    }



    [Test]
    public void InitInteractedWith()
    {
        Assert.IsFalse(appliance.isBeingInteractedWith, "isBeingInteractedWith should be initialised to false");
    }

    [Test]
    public void InitCanUse()
    {
        Assert.IsTrue(appliance.canUse, "canUse should be initialised to true");

    }

   
}
