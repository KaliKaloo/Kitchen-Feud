using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CookingBarTest
{
    CookingBar cookingBar; 

    [OneTimeSetUp]
    public void SetUp() {
        GameObject obj = new GameObject();
        cookingBar = obj.AddComponent<CookingBar>();
    }

    [Test]
    public void keyHeldInit()
    {
        Assert.IsFalse(cookingBar.keyHeld);
    }

    [Test]
    public void doneInit()
    {
        Assert.IsFalse(cookingBar.done);
    }

    [Test]
    public void negAbs()
    {
        float NegAbs = cookingBar.abs(-100f);
        Assert.AreEqual(100f, NegAbs);
    }


    [Test]
    public void posAbs()
    {
        float PosAbs = cookingBar.abs(-100f);
        Assert.AreEqual(100f, PosAbs);
    }

    [Test]
    public void zeroAbs()
    {
        float zeroAbs = cookingBar.abs(0);
        Assert.AreEqual(0, zeroAbs);
    }


    [Test]
    public void negCookedLvl()
    {
        float NegLvl = cookingBar.SetCookedLevel(-50f);
        Assert.AreEqual(50f, NegLvl);
    }

    [Test]
    public void posCookedLvl()
    {
        float PosLvl = cookingBar.SetCookedLevel(20f);
        Assert.AreEqual(80f, PosLvl);
    }

    [Test]
    public void zeroCookedLvl()
    {
        float zeroLvl = cookingBar.SetCookedLevel(0);
        Assert.AreEqual(100, zeroLvl);
    }

}

