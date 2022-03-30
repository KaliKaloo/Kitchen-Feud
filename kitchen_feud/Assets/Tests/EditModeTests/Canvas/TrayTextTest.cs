using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class TrayTextTest
{
    TrayText trayText;

    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        trayText = obj.AddComponent<TrayText>();
        trayText.myText = obj.AddComponent<TextMeshProUGUI>();
    }


    [Test]
    public void TrayTextUpdatesSingleDig()
    {
        trayText.ChangeText("7");
        Assert.AreEqual("Order: 7", trayText.GetText());
    }

    [Test]
    public void TrayTextUpdatesDoubleDig()
    {
        trayText.ChangeText("25");
        Assert.AreEqual("Order: 25", trayText.GetText());
    }

 
}
