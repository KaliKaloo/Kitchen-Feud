using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StoveMinigameCounterTests
{
   StoveMinigameCounter stoveCounter;


    [OneTimeSetUp]
    public void SetUp()
    {
        stoveCounter = new StoveMinigameCounter();
    }


    [Test]
    public void startGame()
    {
        stoveCounter.StartGame();
        Assert.IsFalse(StoveMinigameCounter.end);
    }


    [Test]
    public void endGame()
    {
        stoveCounter.EndGame();
        Assert.IsTrue(StoveMinigameCounter.end);
    }


    [Test]
    public void resetCounter()
    {
        StoveMinigameCounter.ResetCounters();
        Assert.AreEqual(0, StoveMinigameCounter.collisionCounter);
        Assert.AreEqual(0, StoveMinigameCounter.droppedCounter);
        Assert.AreEqual(0, StoveMinigameCounter.correctIngredientCounter);
    }


    [Test]
    public void addCollisionCounter()
    {
        StoveMinigameCounter.ResetCounters();
        stoveCounter.AddCollisionCounter();
        Assert.AreEqual(1, StoveMinigameCounter.collisionCounter);
    }


    [Test]
    public void addDroppedCounter()
    {
        StoveMinigameCounter.ResetCounters();
        stoveCounter.AddDroppedCounter();
        Assert.AreEqual(1, StoveMinigameCounter.droppedCounter);
    }

    [Test]
    public void addCorrectIngredient()
    {
        StoveMinigameCounter.ResetCounters();
        stoveCounter.AddCorrectIngredient();
        Assert.AreEqual(1, StoveMinigameCounter.correctIngredientCounter);
    }

}
