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
    }

    public bool CheckIfFull()
    {
        if (currentIngredients <= 0)
            return true;
        else
            return false;
    }

    public void AddScore()
    {
        if (initialIngredients == 3) {
            score += 1;
        } 
        else if (initialIngredients == 2)
        {
            score += 1.5f;
        } 
        else if (initialIngredients == 1)
        {
            score += 3;
        }
        currentIngredients -= 1;
        Debug.Log(score);
    }

    public void MinusIngredient()
    {
        currentIngredients -= 1;
    }

    public void AddBombMultiplier()
    {
        bombMultiplier += 0.1f;
    }

}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public Text errorTextBomb;
    StoveScore stoveScore = new StoveScore();

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag.ToString() == "Bomb")
        {
            // PLAY BOMB SOUND EFFECT
            StartCoroutine(ShowText("YOU CAUGHT A BOMB"));
            Destroy(target.gameObject);
            stoveScore.AddBombMultiplier();
        }

        if (target.tag.ToString() == "Ingredient")
        {
            Destroy(target.gameObject);
            stoveScore.AddScore();
            if (stoveScore.CheckIfFull())
            {
                // EXIT HERE
            }
        }
    }


    IEnumerator ShowText(string text)
    {
        errorTextBomb.text = text;
        yield return new WaitForSeconds(1);
        errorTextBomb.text = "";

    }
}



