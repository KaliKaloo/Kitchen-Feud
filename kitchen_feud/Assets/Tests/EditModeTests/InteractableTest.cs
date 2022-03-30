using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InteractableTest
{

    Interactable interactable;
    
    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        interactable = obj.AddComponent<Interactable>();
    }


    [Test]
    public void initInteractable()
    {
        Assert.IsFalse(interactable.isFocus, "isFocus should be initialised to false");
    }


    [Test]
    public void onFocusedTest()
    {
        GameObject transObj = new GameObject();
        Transform playerTransform = transObj.transform;
        interactable.OnFocused(playerTransform);
        Assert.IsTrue(interactable.isFocus, "isFocus should be set to true");
        Assert.AreEqual(playerTransform, interactable.player, "interactable.player not assigned correctly");

    }


    [Test]
    public void defocusedTest()
    {
        GameObject transObj = new GameObject();
        Transform playerTransform = transObj.transform;
        interactable.player = playerTransform;
        Assert.IsNotNull(interactable.player);
        interactable.OnDefocused();
        Assert.IsFalse(interactable.isFocus, "isFocus should be set to false");
        Assert.IsNull(interactable.player, "player should be null on defocus");

    }

   
}
