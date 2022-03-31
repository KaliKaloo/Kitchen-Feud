using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnableSmokeTest
{

    EnableSmoke enableSmoke = new EnableSmoke();
    
    
    [Test]
    public void InitSmoke()
    {   
        enableSmoke = new EnableSmoke();
        Assert.IsFalse(enableSmoke.GetPlayerState());
        enableSmoke.SetPlayerState(true);
    }


    [Test]
    public void RestartState()
    {   
        enableSmoke.RestartState();
        Assert.IsFalse(enableSmoke.GetPlayerState());
        Assert.IsFalse(EnableSmoke.usedUp);

    }

    [Test]
    public void UseSmoke()
    {   
        enableSmoke.UseSmoke();
        Assert.IsTrue(EnableSmoke.usedUp);
    }


    [Test]
    public void SetUIForSmoke()
    {   
        GameObject slot = new GameObject();
        enableSmoke.SetUIForSmoke(slot);
        Assert.AreEqual(slot, EnableSmoke.smokeSlot);
        Assert.IsFalse(EnableSmoke.smokeSlot.activeSelf);

    }

    [Test]
    public void DisableSmokeSlot()
    {   
        GameObject slot = new GameObject();
        enableSmoke.SetUIForSmoke(slot);
        EnableSmoke.smokeSlot.SetActive(true);
        enableSmoke.DisableSmokeSlot();
        Assert.IsFalse(EnableSmoke.smokeSlot.activeSelf);
    }


    [Test]
    public void NoChangePlayerState()
    {   
        EnableSmoke.usedUp = true;
        bool prevState = enableSmoke.GetPlayerState();
        enableSmoke.ChangePlayerState(true);
        Assert.AreEqual(prevState, enableSmoke.GetPlayerState());
    }

    [Test]
    public void ChangePlayerStateTrue()
    {   
        GameObject slot = new GameObject();
        enableSmoke.SetUIForSmoke(slot);
        EnableSmoke.usedUp = false;
        enableSmoke.ChangePlayerState(true);
        Assert.IsTrue(enableSmoke.GetPlayerState());
        Assert.IsTrue(EnableSmoke.smokeSlot.activeSelf);
    }


    [Test]
    public void ChangePlayerStateFalse()
    {   
        GameObject slot = new GameObject();
        enableSmoke.SetUIForSmoke(slot);
        EnableSmoke.usedUp = false;
        enableSmoke.ChangePlayerState(false);
        Assert.IsFalse(enableSmoke.GetPlayerState());
        Assert.IsFalse(EnableSmoke.smokeSlot.activeSelf);

    }

    [TearDown]

    public void reset(){
        enableSmoke.SetPlayerState(false);
        EnableSmoke.usedUp = false;
    }

  
}
