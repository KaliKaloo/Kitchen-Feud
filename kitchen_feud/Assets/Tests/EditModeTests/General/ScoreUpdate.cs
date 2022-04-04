using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreUpdate
{


    ParseScore score;

    [OneTimeSetUp]
    public void SetUp()
    {
        score = new ParseScore();
    }


    [Test]
    public void scoresToZero()
    {
        score.UpdateScores(0, 0);
        int updatedScore1 = score.GetScore1();
        int updatedScore2 = score.GetScore2();
        Assert.AreEqual(0, updatedScore1, "score 1 not updated correctly");
        Assert.AreEqual(0, updatedScore2, "score 2 not updated correctly");
    }

    [Test]
    public void scoresPosUpdate()
    {
        score.UpdateScores(5, 4);
        int updatedScore1 = score.GetScore1();
        int updatedScore2 = score.GetScore2();
        Assert.AreEqual(5, updatedScore1, "score 1 not updated correctly");
        Assert.AreEqual(4, updatedScore2, "score 2 not updated correctly");
    }

    [Test]
    public void scoresNegUpdate()
    {
        score.UpdateScores(-3, -29);
        int updatedScore1 = score.GetScore1();
        int updatedScore2 = score.GetScore2();
        Assert.AreEqual(-3, updatedScore1, "score 1 not updated correctly");
        Assert.AreEqual(-29, updatedScore2, "score 2 not updated correctly");
    }

    [Test]
    public void scoresNegPosUpdate()
    {
        score.UpdateScores(-3, 9);
        int updatedScore1 = score.GetScore1();
        int updatedScore2 = score.GetScore2();
        Assert.AreEqual(-3, updatedScore1, "score 1 not updated correctly");
        Assert.AreEqual(9, updatedScore2, "score 2 not updated correctly");
    }

    [Test]
    public void scoresPosNegUpdate()
    {
        score.UpdateScores(7, -2);
        int updatedScore1 = score.GetScore1();
        int updatedScore2 = score.GetScore2();
        Assert.AreEqual(7, updatedScore1, "score 1 not updated correctly");
        Assert.AreEqual(-2, updatedScore2, "score 2 not updated correctly");
    }


    [Test]
    public void AddNegScore1DoesNothing()
    {
        int initialScore1 = score.GetScore1();
        score.AddScore1(-2);
        int updatedScore1 = score.GetScore1();
        Assert.AreEqual(initialScore1, updatedScore1, "Adding a negative score to score 1 should do nothing");
    }

    [Test]
    public void AddNegScore2DoesNothing()
    {
        int initialScore2 = score.GetScore2();
        score.AddScore2(-5);
        int updatedScore2 = score.GetScore2();
        Assert.AreEqual(initialScore2, updatedScore2, "Adding a negative score to score 2 should do nothing");
    }

    [Test]
    public void AddZeroScore1DoesNothing()
    {
        int initialScore1 = score.GetScore1();
        score.AddScore1(0);
        int updatedScore1 = score.GetScore1();
        Assert.AreEqual(initialScore1, updatedScore1, "Adding zero score to score 1 should do nothing");
    }

    [Test]
    public void AddZeroScore2DoesNothing()
    {
        int initialScore2 = score.GetScore2();
        score.AddScore2(0);
        int updatedScore2 = score.GetScore2();
        Assert.AreEqual(initialScore2, updatedScore2, "Adding zero score to score 2 should do nothing");
    }

    [Test]
    public void AddPosScore1()
    {
        int initialScore1 = score.GetScore1();
        score.AddScore1(5);
        int updatedScore1 = score.GetScore1();
        Assert.AreEqual(initialScore1+5, updatedScore1, "Adding score to score 1 not updating correctly");
    }

    [Test]
    public void AddPosScore2()
    {
        int initialScore2 = score.GetScore2();
        score.AddScore2(5);
        int updatedScore2 = score.GetScore2();
        Assert.AreEqual(initialScore2+5, updatedScore2, "Adding score to score 2  not updating correctly");
    }





}