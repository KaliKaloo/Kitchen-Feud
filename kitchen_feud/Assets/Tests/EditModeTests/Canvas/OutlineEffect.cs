using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class OutlineEffectTests
{
    OutlineEffect outlineEffect;
    [OneTimeSetUp]
    public void setUp(){
        GameObject obj = new GameObject();
        outlineEffect = obj.AddComponent<OutlineEffect>();
        outlineEffect.outlineObject = new GameObject();
        outlineEffect.outlineObject.AddComponent<MeshRenderer>();

    }

    [Test]
    public void InitOutline()
    {
        Assert.AreEqual(1.03f, outlineEffect.thickness);
    }


    [Test]
    public void startGlow()
    {
        outlineEffect.startGlowing();
        Assert.AreEqual(true, outlineEffect.outlineObject.GetComponent<Renderer>().enabled);
    }

    [Test]
    public void stopGlow()
    {
        outlineEffect.stopGlowing();
        Assert.AreEqual(false, outlineEffect.outlineObject.GetComponent<Renderer>().enabled);
    }

  
}
