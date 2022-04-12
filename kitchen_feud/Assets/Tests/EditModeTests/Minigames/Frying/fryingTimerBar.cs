using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class fryingTimerBarTests
{

    FryingTimerBar fryingTimerBar;

    [SetUp]
    public void setUp()
    {
        GameObject obj = new GameObject();
        fryingTimerBar = obj.AddComponent<FryingTimerBar>();
    }


    [Test]
    public void SetFriedLevelPosInt()
    {
        float lvl = fryingTimerBar.SetFriedLevel(5);
        Assert.AreEqual(15, lvl);
    }


    [Test]
    public void SetFriedLevelPosFloat()
    {
        float lvl = fryingTimerBar.SetFriedLevel(5.5f);
        Assert.AreEqual(15.5, lvl);
    }


    [Test]
    public void SetFriedLevelNegInt()
    {
        float lvl = fryingTimerBar.SetFriedLevel(15);
        Assert.AreEqual(15, lvl);

    }

    [Test]
    public void SetFriedLevelNegFloat()
    {
        float lvl = fryingTimerBar.SetFriedLevel(15.5f);
        Assert.AreEqual(14.5f, lvl);

    }


  
}
