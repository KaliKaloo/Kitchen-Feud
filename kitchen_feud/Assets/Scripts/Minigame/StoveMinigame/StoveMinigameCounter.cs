using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StoveMinigameCounter
{
    public static int droppedCounter;
    public static bool end;
    public static int collisionCounter;
    public static int correctIngredientCounter;

    public static void ResetCounters()
    {
        droppedCounter = 0;
        collisionCounter = 0;
        correctIngredientCounter = 0;
    }

    public void AddCollisionCounter()
    {
        collisionCounter += 1;
    }

    public void AddCorrectIngredient()
    {
        correctIngredientCounter += 1;
    }

    public void AddDroppedCounter()
    {
        droppedCounter += 1;
    }

    public void StartGame()
    {
        end = false;
        
    }
    public void EndGame()
    {
        end = true;
    }
}
