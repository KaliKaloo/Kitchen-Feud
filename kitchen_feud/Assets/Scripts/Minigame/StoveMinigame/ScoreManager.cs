using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoveScore
{
    private static int initialIngredients;
    private static int currentIngredients;
    private static float score;
    private static float bombMultiplier;


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

}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public Text errorTextBomb;
    [SerializeField] public GameObject backbutton;
    [SerializeField] public Text score;

    StoveScore stoveScore = new StoveScore();
    public StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

    // ingredients get counted if completely fall through top
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag.ToString() == "Ingredient")
        {
            Destroy(target.gameObject);

            stoveScore.AddScore();
            stoveMinigameCounter.MinusCollisionCounter();
            
            score.text = "Score: " + StoveScore.Score + "/15";
                        
            if (stoveMinigameCounter.GetCollisionCounter() == 0)
            {
                backbutton.SetActive(true);
            }
                
        }        
    }

    // bombs will get hit if you just touch them
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag.ToString() == "Bomb")
        {
            Destroy(target.gameObject);
            stoveScore.AddBombMultiplier();
        }
    }
}



