using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public void AddBombMultiplier()
    {
        bombMultiplier += 0.1f;
    }

    public float FinalMultiplier()
    {
        return (score / 15) * (1 - bombMultiplier);
        
    }

    public float CalculateScore()
    {
        return ((float)StoveMinigameCounter.correctIngredientCounter / (float)maximum) * 100f;
    }

}