using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            stoveMinigameCounter.AddCollisionCounter();
            stoveMinigameCounter.AddCorrectIngredient();

            score.text = "Score: " + StoveScore.Score + "/" + StoveScore.maximum;
                        
            if (StoveMinigameCounter.collisionCounter == 0)
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



