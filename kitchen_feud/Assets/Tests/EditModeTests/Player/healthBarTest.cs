using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class healthBarTest
{

    HealthBar health;
    
    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        health = obj.AddComponent<HealthBar>();
        health.slider = obj.AddComponent<Slider>();
        health.fill = obj.AddComponent<Image>();
        health.gradient = new Gradient();
    }

    [Test]
    public void SetMaxHealth()
    {
        health.SetMaxHealth(1);
        Assert.AreEqual(1, health.slider.maxValue);
        Assert.AreEqual(1, health.slider.value);
    }

     [Test]
    public void SetHealthNeg()
    {
        health.SetHealth(-1);
        Assert.AreEqual(0, health.slider.value, "value cannot be negative");
    }


    [Test]
    public void SetHealth()
    {
        health.SetHealth(0.2f);
        Assert.AreEqual(0.2f, health.slider.value);
    }

    
    public void healthMoreThanMax()
    {
        health.SetMaxHealth(0.3f);
        health.SetHealth(0.4f);
        Assert.AreEqual(0.3f, health.slider.value, "slider value cannot exceed max value");
    }

  
}
