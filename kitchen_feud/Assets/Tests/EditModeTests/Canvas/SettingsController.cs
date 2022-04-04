using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SettingsControllerTests
{

    settingsController settingsContr;
    GameObject obj;

    [OneTimeSetUp]
    public void setUp(){
        obj = new GameObject();  
        settingsContr = obj.AddComponent<settingsController>();  

    }

    [Test]
    public void showSettings()
    {
        settingsContr.showSettings();
        Assert.IsTrue(obj.activeSelf);
    }

    [Test]
    public void exitSettings()
    {
        settingsContr.exitSettings();
        Assert.IsFalse(obj.activeSelf);
    }

 
}
