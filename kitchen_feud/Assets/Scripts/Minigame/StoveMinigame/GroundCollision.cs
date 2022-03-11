using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundCollision : MonoBehaviour
{
    StoveScore stoveScore = new StoveScore();
    //[SerializeField] public Text errorTextIngredient;
    [SerializeField] public GameObject backbutton;
    public ScoreManager ScoreManager;

    StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();
    //public StoveMinigameCounter stoveMinigameCounter = ScoreManager.stoveMinigameCounter;

    void OnTriggerEnter2D(Collider2D target)
    {
        //Debug.Log("you missed:"+ScoreManager.stoveMinigameCounter.GetCollisionCounter());
        Destroy(target.gameObject);
        if (target.tag.ToString() == "Ingredient")
        {
            //StartCoroutine(ShowText("You let an ingredient fall!"));
            stoveMinigameCounter.MinusCollisionCounter();
            ScoreManager.totalHits++;      
            ScoreManager.totalmisses++;
             Debug.Log("you missed:" + ScoreManager.totalmisses );

               
        if ((stoveMinigameCounter.GetGameState() == true))
        //if ((stoveMinigameCounter.GetGameState() == true) && ScoreManager.totalHits++ == 15 )
            {
                ScoreManager.StopGame();
            }
        }
      
        //if ((stoveMinigameCounter.GetGameState() == true) && (stoveMinigameCounter.GetCollisionCounter() == 0))
        // if ((stoveMinigameCounter.GetGameState() == true))
        // {
        //     backbutton.gameObject.SetActive(true);
        //     stoveMinigameCounter.StartGame();
        // }
    }

    // IEnumerator ShowText(string text)
    // {
    //     errorTextIngredient.text = text;
    //     yield return new WaitForSeconds(2);
    //     errorTextIngredient.text = "";

    // }

}
