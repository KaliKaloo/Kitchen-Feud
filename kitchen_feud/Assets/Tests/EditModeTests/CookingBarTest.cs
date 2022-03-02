using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CookingBarTest
{
    CookingBar cookingBar = new CookingBar();
   
    [Test]
    public void TestAbs()
    {
        float absolute = cookingBar.abs(-100f);
        Assert.AreEqual(100f, absolute);
    }

    [Test]
    public void NegTestCookedLvl()
    {
        float absolute = cookingBar.SetCookedLevel(-50f);
        Assert.AreEqual(50f, absolute);
    }

    [Test]
    public void PosTestCookedLvl()
    {
        float absolute = cookingBar.SetCookedLevel(20f);
        Assert.AreEqual(80f, absolute);
    }


}

