using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class OvenTimerTests
{

    Timer timer;
    
    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        timer = obj.AddComponent<Timer>();
    }

    [Test]
    public void InitTimer()
    {
        Assert.AreEqual(0, timer.score);
        Assert.AreEqual(timer.GetTotalTime(), timer.timer);
        Assert.AreEqual(timer.GetTotalTime(), timer.timerFake);
    }

    [Test]
    public void ChangeTimerValuePos()
    {
        timer.ChangeTimerValue(50);
        Assert.AreEqual(50, timer.timer);
        Assert.AreEqual(50, timer.GetTotalTime());
    }

    [Test]
    public void ChangeTimerValueNeg()
    {
        timer.ChangeTimerValue(-10);
        Assert.AreEqual(0, timer.timer);
        Assert.AreEqual(0, timer.GetTotalTime());

    }


    [Test]
    public void SecondToWholeMinutes()
    {
        string mins = timer.ConvertSecondToMinutes(300);
        Assert.AreEqual("05:00", mins, "seconds not converted to minutes correctly");
    }


    [Test]
    public void SecondToZeroDecMinutes()
    {
        string mins = timer.ConvertSecondToMinutes(30);
        Assert.AreEqual("00:30", mins, "seconds not converted to minutes correctly");
    }

    [Test]
    public void SecondToDecMinutes()
    {
        string mins = timer.ConvertSecondToMinutes(330);
        Assert.AreEqual("05:30", mins, "seconds not converted to minutes correctly");
    }


    [Test]
    public void NegSecondToWholeMinutes()
    {
        string mins = timer.ConvertSecondToMinutes(-300);
        Assert.AreEqual("-05:00", mins, "seconds not converted to minutes correctly");
    }


    [Test]
    public void NegSecondToZeroDecMinutes()
    {
        string mins = timer.ConvertSecondToMinutes(-30);
        Assert.AreEqual("-00:30", mins, "seconds not converted to minutes correctly");
    }


    [Test]
    public void NegSecondToDecMinutes()
    {
        string mins = timer.ConvertSecondToMinutes(-330);
        Assert.AreEqual("-05:30", mins, "seconds not converted to minutes correctly");
    }

    [Test]
    public void ZeroSecondsConversion()
    {
        string mins = timer.ConvertSecondToMinutes(0);
        Assert.AreEqual("00:00", mins, "seconds not converted to minutes correctly");
    }



    [Test]
    public void InitializeTimer()
    {
        timer.InitializeTimer();
        Assert.AreEqual(timer.GetTotalTime(), timer.timer);
        Assert.AreEqual(timer.GetTotalTime(), timer.timerFake);

    }


    [Test]
    public void PosDecSameTime()
    {
        timer.timer = timer.timerFake = 10;
        timer.Decrement();
        Assert.AreEqual(9, timer.timer);
        Assert.AreEqual(9, timer.timerFake);
        Assert.AreEqual(2, timer.score);
    }

    [Test]
    public void NegDecSameTime()
    {
        timer.timer = timer.timerFake = -1;
        timer.Decrement();
        Assert.AreEqual(-2, timer.timer);
        Assert.AreEqual(-2, timer.timerFake);
        Assert.AreEqual(-2, timer.score);
    }


    [Test]
    public void ZeroDecSameTime()
    {
        timer.timer = timer.timerFake = 0;
        timer.Decrement();
        Assert.AreEqual(-1, timer.timer);
        Assert.AreEqual(-1, timer.timerFake);
        Assert.AreEqual(-2, timer.score);


    }


    [Test]
    public void DecFakeLessThanRealTime()
    {
        timer.timer = 5;
        timer.timerFake = -5;
        timer.Decrement();
        Assert.AreEqual(4, timer.timer);
        Assert.AreEqual(-6, timer.timerFake);
        Assert.AreEqual(2, timer.score);
    }

    [Test]
    public void DecFakeMoreThanRealTime()
    {
        timer.timer = -5;
        timer.timerFake = 5;
        timer.Decrement();
        Assert.AreEqual(-6, timer.timer);
        Assert.AreEqual(4, timer.timerFake);
        Assert.AreEqual(-2, timer.score);
    }


    [Test]
    public void MultipleDec()
    {
        timer.timer = 4;
        timer.timerFake = 4;
        timer.Decrement();
        timer.Decrement();
        timer.Decrement();
        Assert.AreEqual(1, timer.timer);
        Assert.AreEqual(1, timer.timerFake);
        Assert.AreEqual(6, timer.score);
    }

    [Test]
    public void MultipleDecAddMinusScore()
    {
        timer.timer = 2;
        timer.timerFake = 2;
        timer.Decrement();
        timer.Decrement();
        timer.Decrement();
        Assert.AreEqual(-1, timer.timer);
        Assert.AreEqual(-1, timer.timerFake);
        Assert.AreEqual(2, timer.score);
    }





}
