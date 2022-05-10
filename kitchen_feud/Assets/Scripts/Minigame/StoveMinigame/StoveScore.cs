using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Setting the score for stove minigame to be parsed to other scripts
public class StoveScore
{
    private static int initialIngredients;
    private static int currentIngredients;
    private static float score;
    private static float bombMultiplier;

    public static readonly int maximum = 10;

    public static float Score
    {
        get{  return score; }
        set
        {
            score = value;
        }
    }

    public static float BombMultiplier
    {
        get{  return bombMultiplier; }
        set
        {
            bombMultiplier = value;
        }
    }

    public static int InitialIngredients
    {
        get{  return initialIngredients; }
    }


    public static int CurrentIngredients
    {
        get{  return currentIngredients; }
    }

    public void SetAmountInitialIngredients(int amount)
    {
        initialIngredients = amount;
        currentIngredients = amount;
    }

    public void ResetValues()
    {
        initialIngredients = currentIngredients = 3;
        score = 0;
        bombMultiplier = 0;
    }

    public void AddScore()
    {
        score += 1;
    }

    // calculate score based on how many correct ingredients caught
    public float CalculateScore()
    {
        return ((float)StoveMinigameCounter.correctIngredientCounter / (float)maximum) * 100f;
    }

}