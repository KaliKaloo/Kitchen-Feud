using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TeamSetup
{
    GameSetup gameSetup; 

    [OneTimeSetUp]
    public void SetUp() {
        GameObject obj = new GameObject();
        gameSetup = obj.AddComponent<GameSetup>();
    }

    [Test]
    public void nextPlayerInTeam1()
    {
        gameSetup.nextPlayersTeam = 2;
        gameSetup.UpdateTeam();
        Assert.AreEqual(1, gameSetup.nextPlayersTeam, "player assigned to wrong team");
    }

    [Test]
    public void nextPlayerInTeam2()
    {
        gameSetup.nextPlayersTeam = 1;
        gameSetup.UpdateTeam();
        Assert.AreEqual(2, gameSetup.nextPlayersTeam, "player assigned to wrong team");
    }

    
}
