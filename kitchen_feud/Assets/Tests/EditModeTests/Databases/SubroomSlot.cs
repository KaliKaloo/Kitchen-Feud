using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SubroomSlotTest
{

    
    SubroomSlot subroomSlot;
    
    [Test]
    public void IngredientAddPos()
    {
        IngredientSO item = ScriptableObject.CreateInstance<IngredientSO>();
        subroomSlot = new SubroomSlot(item, 10);
        subroomSlot.AddAmount(5);
        Assert.AreEqual(15, subroomSlot.amount);
    }


     [Test]
    public void IngredientAddZero()
    {
        IngredientSO item = ScriptableObject.CreateInstance<IngredientSO>();
        subroomSlot = new SubroomSlot(item, 10);
        subroomSlot.AddAmount(0);
        Assert.AreEqual(10, subroomSlot.amount);
    }

    [Test]
    public void IngredientAddNeg()
    {
        IngredientSO item = ScriptableObject.CreateInstance<IngredientSO>();
        subroomSlot = new SubroomSlot(item, 10);
        subroomSlot.AddAmount(-20);
        Assert.AreEqual(-10, subroomSlot.amount);
    } 



    [Test]
    public void DishAddPos()
    {
        DishSO item = ScriptableObject.CreateInstance<DishSO>();
        subroomSlot = new SubroomSlot(item, 20);
        subroomSlot.AddAmount(15);
        Assert.AreEqual(35, subroomSlot.amount);
    }


     [Test]
    public void DishAddZero()
    {
        DishSO item = ScriptableObject.CreateInstance<DishSO>();
        subroomSlot = new SubroomSlot(item, 20);
        subroomSlot.AddAmount(0);
        Assert.AreEqual(20, subroomSlot.amount);
    }

    [Test]
    public void DishAddNeg()
    {
        DishSO item = ScriptableObject.CreateInstance<DishSO>();
        subroomSlot = new SubroomSlot(item, 20);
        subroomSlot.AddAmount(-10);
        Assert.AreEqual(10, subroomSlot.amount);
    } 
}
