using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CookingBarTest
{
    CookingBar cookingBar = new CookingBar();

   
    // A Test behaves as an ordinary method
    [Test]
    public void CookingBarTestSimplePasses()
    {
        // Use the Assert class to test conditions
         float absolute = cookingBar.abs(-100f);
        Assert.AreEqual(100f, absolute);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CookingBarTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}

