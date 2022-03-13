using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Timer
{

    GlobalTimer timer = new GlobalTimer();

    [Test]
    public void TimerNotNull()
    {
        int currTime = timer.GetCurrentTime();
        Assert.NotNull(currTime, "time should not be null");
    }


    [Test]
    public void ChangeTimerZero()
    {
        timer.ChangeTimerValue(0);
        int currTime = timer.GetCurrentTime();
        Assert.AreEqual(0, currTime, "time should be set to 0");
    }

    [Test]
    public void ChangeTimerValue()
    {
        timer.ChangeTimerValue(300);
        int currTime = timer.GetCurrentTime();
        Assert.AreEqual(300, currTime, "changing timer values does not update correctly");
    }

    [Test]
    public void ChangeTimerNeg()
    {
        timer.ChangeTimerValue(-5);
        int currTime = timer.GetCurrentTime();
        Assert.AreEqual(0, currTime, "time should be 0 if set to negative");
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

}