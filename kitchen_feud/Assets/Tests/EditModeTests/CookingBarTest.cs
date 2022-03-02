using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CookingBarTest
{
    CookingBar cookingBar = new CookingBar();

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


}

