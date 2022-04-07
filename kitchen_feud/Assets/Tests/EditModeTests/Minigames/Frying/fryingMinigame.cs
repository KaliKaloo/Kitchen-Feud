using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class fryingMinigameTests
{
    fryingMinigame fryingMinigame;

    [OneTimeSetUp]
    public void setUp()
    {
        GameObject obj = new GameObject();
        fryingMinigame = obj.AddComponent<fryingMinigame>();

    }


    [Test]
    public void getPattySpriteName()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        dish.dishID = "DI1415";
        string spriteName = fryingMinigame.GetSpriteName(dish);
        Assert.AreEqual("patty", spriteName);
    }


    [Test]
    public void getRiceSpriteName()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        dish.dishID = "DI1621";
        string spriteName = fryingMinigame.GetSpriteName(dish);
        Assert.AreEqual("rice", spriteName);
    }


    [Test]
    public void getEggySpriteName()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        dish.dishID = "DI1316";
        string spriteName = fryingMinigame.GetSpriteName(dish);
        Assert.AreEqual("Eggy bread", spriteName);
    }

    [Test]
    public void geteggFriedSpriteName()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        dish.dishID = "DI1617";
        string spriteName = fryingMinigame.GetSpriteName(dish);
        Assert.AreEqual("eggFried", spriteName);
    }


    [Test]
    public void getPancakeSpriteName()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        dish.dishID = "DI163134";
        string spriteName = fryingMinigame.GetSpriteName(dish);
        Assert.AreEqual("Pancake", spriteName);
    }

}
