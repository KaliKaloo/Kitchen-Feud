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
    }

    public void MinusIngredient()
    {
        currentIngredients -= 1;
    }

    public void AddBombMultiplier()
    {
        bombMultiplier += 0.1f;
    }

    public float FinalMultipier()
    {
        return Mathf.Clamp(((score / 6) + 0.5f) * (1 - bombMultiplier), 0.2f, 1);
    }

}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public Text errorTextBomb;
    StoveScore stoveScore = new StoveScore();

    //void OnTriggerEnter2D(Collider2D target)
    //{
    //    Destroy(target.gameObject);
    //    if (target.tag.ToString() == "Bomb")
    //    {
    //        // PLAY BOMB SOUND EFFECT
    //        StartCoroutine(ShowText("YOU CAUGHT A BOMB"));
    //        stoveScore.AddBombMultiplier();
    //    }

    //    if (target.tag.ToString() == "Ingredient")
    //    {
    //        stoveScore.AddScore();
    //    }
    //}

    void OnTriggerExit2D(Collider2D target)
    {
        Destroy(target.gameObject);
        if (target.tag.ToString() == "Bomb")
        {
            // PLAY BOMB SOUND EFFECT
            StartCoroutine(ShowText("YOU CAUGHT A BOMB"));
            stoveScore.AddBombMultiplier();
        }

        if (target.tag.ToString() == "Ingredient")
        {
            stoveScore.AddScore();
        }
    }


    IEnumerator ShowText(string text)
    {
        errorTextBomb.text = text;
        yield return new WaitForSeconds(1);
        errorTextBomb.text = "";

    }
}



