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
    public void TestNegAbs()
    {
        float NegAbs = cookingBar.abs(-100f);
        Assert.AreEqual(100f, NegAbs);
    }


    [Test]
    public void TestPosAbs()
    {
        float PosAbs = cookingBar.abs(-100f);
        Assert.AreEqual(100f, PosAbs);
    }

    [Test]
    public void TestZeroAbs()
    {
        float zeroAbs = cookingBar.abs(0);
        Assert.AreEqual(0, zeroAbs);
    }


    [Test]
    public void NegTestCookedLvl()
    {
        float NegLvl = cookingBar.SetCookedLevel(-50f);
        Assert.AreEqual(50f, NegLvl);
    }

    [Test]
    public void PosTestCookedLvl()
    {
        float PosLvl = cookingBar.SetCookedLevel(20f);
        Assert.AreEqual(80f, PosLvl);
    }

    [Test]
    public void ZeroTestCookedLvl()
    {
        float zeroLvl = cookingBar.SetCookedLevel(0);
        Assert.AreEqual(100, zeroLvl);
    }


}

