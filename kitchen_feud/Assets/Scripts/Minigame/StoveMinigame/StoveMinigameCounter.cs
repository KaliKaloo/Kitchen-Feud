using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StoveMinigameCounter
{
    public static int counter;
    public static bool end;
    public static int collisionCounter;

    public void MinusCollisionCounter()
    {
        collisionCounter -= 1;
    }

    public int GetCollisionCounter()
    {
        return collisionCounter;
    }

    public void StartGame()
    {
        end = false;
        
    }
    public void EndGame()
    {
        end = true;
        //Spawner.backButton.SetActive(true);
    }

    public bool GetGameState()
    {
        return end;
    }

    public void ResetCounter()
    {
        counter = 15;
        collisionCounter = 15;
    }

    public void MinusCounter()
    {
        counter -= 1;
    }

    public int GetCounter()
    {
        return counter;
    }
}
