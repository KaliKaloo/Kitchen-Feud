using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreInit
{
    ParseScore score;

    [OneTimeSetUp]
    public void SetUp()
    {
        score = new ParseScore();
    }

    [Test]
    public void score1InitZero()
    {
        int initialScore = score.GetScore1();
        Assert.AreEqual(0, initialScore, "score 1 not initialised to 0");
    }

    [Test]
    public void score2InitZero()
    {
        int initialScore = score.GetScore2();
        Assert.AreEqual(0, initialScore, "score 2 not initialised to 0");
    }
}