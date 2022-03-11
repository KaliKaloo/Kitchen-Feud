using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoveScore
{
    public static int initialIngredients;
    public static int currentIngredients;
    public static float score;
    public static float bombMultiplier;


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

    public bool CheckIfFull()
    {
        if (currentIngredients <= 0)
        {
            Debug.Log(this.FinalMultipier());
            return true;
        }
        else
            return false;
    }

    public void AddScore()
    {
        score += 1;
    }

    public int GetScore()
    {
        return (int)score;
    }

    public void AddBombMultiplier()
    {
        bombMultiplier += 0.1f;
    }

    public float FinalMultipier()
    {
        return (score / 15) * (1 - bombMultiplier);
        
    }

}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public Text errorTextBomb;
    [SerializeField] public GameObject backbutton;
    [SerializeField] public Text score;
    public int totalHits;
    public int totalmisses;
    private int caught;

    StoveScore stoveScore = new StoveScore();
    public StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

    // ingredients get counted if completely fall through top
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag.ToString() == "Ingredient")
        {
            Destroy(target.gameObject);
            totalHits++;
            caught++;

            stoveScore.AddScore();
            stoveMinigameCounter.MinusCollisionCounter();
            
            score.text = "Score: " + stoveScore.GetScore() + "/15";
            
            Debug.Log("you caught:" + caught);
            
            if ((stoveMinigameCounter.GetGameState() == true))
            {
                    StopGame();
            }
                
        }
       
       //if (stoveMinigameCounter.GetCollisionCounter() == 14)
        
    }

    public void StopGame(){
        Debug.Log(stoveMinigameCounter.GetCollisionCounter());
        
        if (stoveMinigameCounter.GetCollisionCounter() == 15) 
        {
            backbutton.SetActive(true);
         //stoveMinigameCounter.StartGame();
        }
    }

    // bombs will get hit if you just touch them
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag.ToString() == "Bomb")
        {
            Destroy(target.gameObject);
            //StartCoroutine(ShowText("YOU HIT A BOMB"));
            stoveScore.AddBombMultiplier();
        }
    }

    // IEnumerator ShowText(string text)
    // {
    //     errorTextBomb.text = text;
    //     yield return new WaitForSeconds(1);
    //     errorTextBomb.text = "";

    // }
}



