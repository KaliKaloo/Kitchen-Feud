using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    private int zeroCount;

    private bool started;
   // public int nextPlayersTeam;
    public Transform[] spawnPoints1;
    public Transform[] spawnPoints2;

    private void Start()
    {
        started = false;
        zeroCount = 0;
    }

    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    private void Update()
    {
  
    }

/*
    public void UpdateTeam()
    {
        if (nextPlayersTeam == 1)
        {
            nextPlayersTeam = 2;
        }
        else
        {
            nextPlayersTeam = 1;
        }

    }
*/
}