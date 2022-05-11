using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StoveMinigameCounter
{
    public static int amount = 10;

    // counts from 0 -> amount how many items have been dropped from the ceiling
    public static int droppedCounter;

    // counts from 0 -> amount how many items have been caught by the pot
    // once reached amount the game should end
    public static int collisionCounter;

    // counts from 0 -> amount how many correct ingredients have been caught
    public static int correctIngredientCounter;

    public static bool end;

    // call then when game ends/starts
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

    // signal to start stove minigame
    public void StartGame()
    {
        end = false;

    }

    // sets the state of the game to end
    public void EndGame()
    {
        end = true;
    }
}
