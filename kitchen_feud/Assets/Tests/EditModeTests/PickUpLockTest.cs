using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PickUpLockTest
{
    PickupLock pickup;

    [SetUp]
    public void SetUp()
    {
        pickup = new PickupLock();
    }

    [Test]
    public void InitPickup()
    {
        Assert.IsFalse(pickup.GetLock(), "pickupLock should be initialised to false");
    }


    [Test]
    public void Lock()
    {
        pickup.Lock();
        Assert.IsTrue(pickup.GetLock(), "pickupLock should be updated to true");
    }

    [Test]
    public void UnLock()
    {
        pickup.Lock();
        pickup.Unlock();
        Assert.IsFalse(pickup.GetLock(), "pickupLock should be updated to false");
    }

   
}
