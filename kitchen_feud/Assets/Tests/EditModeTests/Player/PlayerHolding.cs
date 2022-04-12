using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



public class PlayerHoldingTest
{
    PlayerHolding playerHolding;
    [OneTimeSetUp]
    public void setUp(){
        GameObject obj = new GameObject();
        playerHolding = obj.AddComponent<PlayerHolding>();
    }

    [Test]
    public void InitPlayerHold()
    {
        Assert.AreEqual(1, playerHolding.holdingLimit);
        Assert.IsFalse(playerHolding.itemdropped);
        Assert.IsFalse(playerHolding.itemLock);
    }

  
}
