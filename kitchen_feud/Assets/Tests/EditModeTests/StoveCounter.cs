using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StoveCounter
{
   StoveMinigameCounter stoveCounter = new StoveMinigameCounter();

    [Test]
    public void startGame()
    {
        stoveCounter.StartGame();
        Assert.IsFalse(stoveCounter.GetGameState());
    }


    [Test]
    public void endGame()
    {
        stoveCounter.EndGame();
        Assert.IsTrue(stoveCounter.GetGameState());
    }


    [Test]
    public void resetCounter()
    {
        stoveCounter.ResetCounter();
        Assert.AreEqual(15, stoveCounter.GetCounter());
        Assert.AreEqual(15, stoveCounter.GetCollisionCounter());
    }


    [Test]
    public void minusCollisionCounter()
    {
        stoveCounter.ResetCounter();
        stoveCounter.MinusCollisionCounter();
        Assert.AreEqual(14, stoveCounter.GetCollisionCounter());
    }


    [Test]
    public void minusCounter()
    {
        stoveCounter.ResetCounter();
        stoveCounter.MinusCounter();
        Assert.AreEqual(14, stoveCounter.GetCounter());
    }

    
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator StoveSpawnerWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

    }
}
