using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SubroomObjectTest
{
    SubroomObject subroomObject;
    
    [SetUp]
    public void SetUp()
    {
        subroomObject = ScriptableObject.CreateInstance<SubroomObject>();
    }


    [Test]
    public void AddsNewIngredient()
    {
        IngredientSO ingredient = ScriptableObject.CreateInstance<IngredientSO>();
        subroomObject.AddItem(ingredient, 5);
        Assert.AreEqual(1, subroomObject.Container.Count);
        Assert.AreEqual(5, subroomObject.Container[0].amount);
    }

    [Test]
    public void UpdatesExistingIngredient()
    {
        IngredientSO ingredient = ScriptableObject.CreateInstance<IngredientSO>();
        subroomObject.AddItem(ingredient, 2);
        Assert.AreEqual(1, subroomObject.Container.Count);
        subroomObject.AddItem(ingredient, 2);
        Assert.AreEqual(1, subroomObject.Container.Count);
        Assert.AreEqual(4, subroomObject.Container[0].amount);
    }


    [Test]
    public void Add2Ingredients()
    {
        IngredientSO ingredient = ScriptableObject.CreateInstance<IngredientSO>();
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        subroomObject.AddItem(ingredient, 3);
        Assert.AreEqual(1, subroomObject.Container.Count);
        subroomObject.AddItem(ingredient2, 2);
        Assert.AreEqual(2, subroomObject.Container.Count);
        Assert.AreEqual(3, subroomObject.Container[0].amount);
        Assert.AreEqual(2, subroomObject.Container[1].amount);
    }


    [Test]
    public void Update2IngredientsOrdered()
    {
        IngredientSO ingredient = ScriptableObject.CreateInstance<IngredientSO>();
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        subroomObject.AddItem(ingredient, 3);
        subroomObject.AddItem(ingredient2, 2);
        Assert.AreEqual(2, subroomObject.Container.Count);
        Assert.AreEqual(3, subroomObject.Container[0].amount);
        Assert.AreEqual(2, subroomObject.Container[1].amount);
        subroomObject.AddItem(ingredient, 4);
        subroomObject.AddItem(ingredient2, 7);
        Assert.AreEqual(2, subroomObject.Container.Count);
        Assert.AreEqual(7, subroomObject.Container[0].amount);
        Assert.AreEqual(9, subroomObject.Container[1].amount);
    }


    [Test]
    public void Update2IngredientsNoOrder()
    {
        IngredientSO ingredient = ScriptableObject.CreateInstance<IngredientSO>();
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        subroomObject.AddItem(ingredient, 3);
        subroomObject.AddItem(ingredient2, 2);
        Assert.AreEqual(2, subroomObject.Container.Count);
        Assert.AreEqual(3, subroomObject.Container[0].amount);
        Assert.AreEqual(2, subroomObject.Container[1].amount);
        subroomObject.AddItem(ingredient2, 3);
        subroomObject.AddItem(ingredient, 5);
        Assert.AreEqual(2, subroomObject.Container.Count);
        Assert.AreEqual(8, subroomObject.Container[0].amount);
        Assert.AreEqual(5, subroomObject.Container[1].amount);

    }


    [Test]
    public void UpdateMultipleTimes()
    {
        IngredientSO ingredient = ScriptableObject.CreateInstance<IngredientSO>();
        subroomObject.AddItem(ingredient, 3);
        subroomObject.AddItem(ingredient, 6);
        subroomObject.AddItem(ingredient, 5);
        Assert.AreEqual(1, subroomObject.Container.Count);
        Assert.AreEqual(14, subroomObject.Container[0].amount);
    }
}
