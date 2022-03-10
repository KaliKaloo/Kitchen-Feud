using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundCollision : MonoBehaviour
{
    StoveScore stoveScore = new StoveScore();
    [SerializeField] public Text errorTextIngredient;
    [SerializeField] public GameObject backbutton;

    StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

    void OnTriggerEnter2D(Collider2D target)
    {
        Destroy(target.gameObject);
        if (target.tag.ToString() == "Ingredient")
        {
            StartCoroutine(ShowText("You let an ingredient fall!"));
            stoveMinigameCounter.MinusCollisionCounter();
        }

        if (stoveMinigameCounter.GetGameState() && stoveMinigameCounter.GetCollisionCounter() == 0)
        {
            backbutton.gameObject.SetActive(true);
            stoveMinigameCounter.StartGame();
        }
    }

    IEnumerator ShowText(string text)
    {
        errorTextIngredient.text = text;
        yield return new WaitForSeconds(2);
        errorTextIngredient.text = "";

    }

}
