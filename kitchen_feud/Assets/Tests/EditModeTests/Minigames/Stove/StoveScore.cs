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
        Assert.AreEqual(0, StoveScore.BombMultiplier);

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

    [Test]
    public void AddBombMultiplier(){
        StoveScore.BombMultiplier = 0.5f;
        stoveScore.AddBombMultiplier();
        Assert.AreEqual(0.6f, StoveScore.BombMultiplier);
    }


    [Test]
    public void FinalMultiplier(){
        StoveScore.Score = 3;
        StoveScore.BombMultiplier = 0.5f;
        float finalScore = stoveScore.FinalMultiplier();
        Assert.AreEqual(0.1f, finalScore);
    }




}
