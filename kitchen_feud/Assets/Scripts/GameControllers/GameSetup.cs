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
    public GameObject loadingCanvas;
    private bool started;
    public Transform[] spawnPoints1;
    public Transform[] spawnPoints2;
    public Transform[] WSP1;
    public Transform[] WSP2;
    public Transform OSP1;
    public Transform OSP2;

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

}