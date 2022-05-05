using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class TimerTests
{

    GlobalTimer timer;


    [OneTimeSetUp]
    public void SetUp()
    {
        timer = new GlobalTimer();
    }

    [Test]
    public void TimerNotNull()
    {
        int currTime = timer.GetLocalTime();
        Assert.NotNull(currTime, "time should not be null");
        Assert.AreEqual(timer.GetTotalTime(), currTime, "time should not be null");

    }


    [Test]
    public void ChangeTimerZero()
    {
        timer.ChangeTimerValue(0);
        Assert.AreEqual(0, timer.GetLocalTime(), "time should be set to 0");
        Assert.AreEqual(0, timer.GetTotalTime(), "time should be set to 0");

    }

    [Test]
    public void ChangeTimerValue()
    {
        timer.ChangeTimerValue(300);
        Assert.AreEqual(300, timer.GetLocalTime(), "changing timer values does not update correctly");
        Assert.AreEqual(300, timer.GetTotalTime());
    }

    [Test]
    public void ChangeTimerNeg()
    {
        timer.ChangeTimerValue(-5);
        Assert.AreEqual(0, timer.GetLocalTime(), "time should be 0 if set to negative");
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
    public void GetCurrentTimeString()
    {
        timer.ChangeTimerValue(300);
        string timeString = timer.GetCurrentTimeString();
        Assert.AreEqual("05:00", timeString);
    }


    
    [Test]
    public void AddShort()
    {
        timer.ChangeTimerValue(20);
        int val = timer.AddSubtractTimerValue(10);
        Assert.AreEqual(1, val);
        Assert.AreEqual(20, timer.GetLocalTime());
        Assert.AreEqual(20, timer.GetTotalTime());
    }


    [Test]
    public void AddShortToMid()
    {
        timer.ChangeTimerValue(20);
        int val = timer.AddSubtractTimerValue(50);
        Assert.AreEqual(0, val);
        Assert.AreEqual(70, timer.GetLocalTime());
        Assert.AreEqual(70, timer.GetTotalTime());
    }

    [Test]
    public void AddShortToLong()
    {
        timer.ChangeTimerValue(20);
        int val = timer.AddSubtractTimerValue(1200);
        Assert.AreEqual(2, val);
        Assert.AreEqual(20, timer.GetLocalTime());
        Assert.AreEqual(20, timer.GetTotalTime());
    }

    [Test]
    public void AddMid()
    {
        timer.ChangeTimerValue(70);
        int val = timer.AddSubtractTimerValue(10);
        Assert.AreEqual(0, val);
        Assert.AreEqual(80, timer.GetLocalTime());
        Assert.AreEqual(80, timer.GetTotalTime());
    }

    [Test]
    public void AddMidToLong()
    {
        timer.ChangeTimerValue(70);
        int val = timer.AddSubtractTimerValue(1200);
        Assert.AreEqual(2, val);
        Assert.AreEqual(70, timer.GetLocalTime());
        Assert.AreEqual(70, timer.GetTotalTime());
    }

    [Test]
    public void AddLong()
    {
        timer.ChangeTimerValue(1200);
        int val = timer.AddSubtractTimerValue(20);
        Assert.AreEqual(2, val);
        Assert.AreEqual(1200, timer.GetLocalTime());
        
        Assert.AreEqual(1200, timer.GetTotalTime());

    }

    [Test]
    public void SubtractLong()
    {
        timer.ChangeTimerValue(1250);
        int val = timer.AddSubtractTimerValue(-49);
        Assert.AreEqual(2, val);
        Assert.AreEqual(1250, timer.GetLocalTime());
        Assert.AreEqual(1250, timer.GetTotalTime());
    }

    [Test]
    public void SubtractLongToMid()
    {
        timer.ChangeTimerValue(1250);
        int val = timer.AddSubtractTimerValue(-100);
        Assert.AreEqual(0, val);
        Assert.AreEqual(1150, timer.GetLocalTime());
        Assert.AreEqual(1150, timer.GetTotalTime());
    }

    [Test]
    public void SubtractLongToShort()
    {
        timer.ChangeTimerValue(1250);
        int val = timer.AddSubtractTimerValue(-1200);
        Assert.AreEqual(1, val);
        Assert.AreEqual(1250, timer.GetLocalTime());
        Assert.AreEqual(1250, timer.GetTotalTime());
    }

    [Test]
    public void SubtractMid()
    {
        timer.ChangeTimerValue(300);
        int val = timer.AddSubtractTimerValue(-50);
        Assert.AreEqual(0, val);
        Assert.AreEqual(250, timer.GetLocalTime());
        Assert.AreEqual(250, timer.GetTotalTime());
    }

    [Test]
    public void SubtractMidToShort()
    {
        timer.ChangeTimerValue(300);
        int val = timer.AddSubtractTimerValue(-260);
        Assert.AreEqual(1, val);
        Assert.AreEqual(300, timer.GetLocalTime());
        Assert.AreEqual(300, timer.GetTotalTime());
    }

}