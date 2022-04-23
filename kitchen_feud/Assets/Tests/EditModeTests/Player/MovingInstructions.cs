/*
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovingInstructionsTests
{

    MovingInstructions movingInstructions;

    [OneTimeSetUp]
    public void setUp()
    {
        GameObject obj = new GameObject();
        movingInstructions = obj.AddComponent<MovingInstructions>();
    }


    [Test]
    public void Decrement()
    {
        float time1 = movingInstructions.timer;
        movingInstructions.Decrement();
        Assert.IsTrue(time1 > movingInstructions.timer);
    }


    [Test]
    public void InitializeTimer()
    {
        movingInstructions.InitializeTimer();
        Assert.AreEqual(13f, movingInstructions.timer);
    }

   
}
*/
