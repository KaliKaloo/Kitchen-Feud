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
        // if game has not ended count the points
        if (!StoveMinigameCounter.end)
        {
            if (StoveMinigameCounter.collisionCounter < 20)
            {
                stoveMinigameCounter.AddCollisionCounter();

                if (target.tag.ToString() == "Ingredient")
                {
                    Destroy(target.gameObject);

                    stoveScore.AddScore();
                    stoveMinigameCounter.AddCorrectIngredient();

                }
            } else
            {
                backbutton.SetActive(true);
                stoveMinigameCounter.EndGame();
            }
        }
    }

    private void Update()
    {
        score.text = "Caught: " + StoveMinigameCounter.collisionCounter + "/" + StoveScore.maximum;
    }
}



