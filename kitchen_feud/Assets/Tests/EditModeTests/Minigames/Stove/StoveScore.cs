using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StoveScoreTests
{

    StoveScore stoveScore;

    [OneTimeSetUp]
    public void setUp()
    {
        stoveScore = new StoveScore();
    }


    [Test]
    public void SetAmountInitialIngredients(){
        stoveScore.SetAmountInitialIngredients(5);
        Assert.AreEqual(5, StoveScore.InitialIngredients);
        Assert.AreEqual(5, StoveScore.CurrentIngredients);
    }

    [Test]
    public void ResetValues(){
        stoveScore.ResetValues();
        Assert.AreEqual(3, StoveScore.InitialIngredients);
        Assert.AreEqual(3, StoveScore.CurrentIngredients);

    }

    [Test]
    public void AddScorePos(){
        StoveScore.Score = 10;
        stoveScore.AddScore();
        Assert.AreEqual(11, StoveScore.Score);
    }

    [Test]
    public void AddScoreNeg(){
        StoveScore.Score = -10;
        stoveScore.AddScore();
        Assert.AreEqual(-9, StoveScore.Score);
    }

}
