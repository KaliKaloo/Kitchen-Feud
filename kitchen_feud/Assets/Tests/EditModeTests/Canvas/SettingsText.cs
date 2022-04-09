using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class SettingsTextTests
{
    settingsText settingsText;
    GameObject obj;
    GameObject grandParent;

    [OneTimeSetUp]
    public void setUp(){
        obj = new GameObject();  
        GameObject parent = new GameObject(); 
        grandParent = new GameObject();  
        parent.transform.SetParent(grandParent.transform);
        obj.transform.SetParent(parent.transform);


        settingsText = obj.AddComponent<settingsText>();  
        obj.AddComponent<TextMeshProUGUI>();  

    }

    [Test]
    public void updateTextNotRotation()
    {
        grandParent.name = "Mvmt";
        settingsText.updateText(0.5f);
        Assert.AreEqual("0.5", obj.GetComponent<TextMeshProUGUI>().text);
    }

    [Test]
    public void updateIntTextNotRotation()
    {
        grandParent.name = "Mvmt";
        settingsText.updateText(1);
        Assert.AreEqual("1", obj.GetComponent<TextMeshProUGUI>().text);
    }


    [Test]
    public void updateTextRotation()
    {
        grandParent.name = "Rotation";
        settingsText.updateText(50);
        Assert.AreEqual("0.8", obj.GetComponent<TextMeshProUGUI>().text);
    }

    [Test]
    public void updateIntTextRotation()
    {
        grandParent.name = "Rotation";
        settingsText.updateText(60);
        Assert.AreEqual("1", obj.GetComponent<TextMeshProUGUI>().text);
    }

  
}
