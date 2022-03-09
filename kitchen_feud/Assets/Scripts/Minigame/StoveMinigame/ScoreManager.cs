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
            GameEvents.current.assignPointsEventFunction();
            return true;
        }
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
    [SerializeField] public GameObject backbutton;

    StoveScore stoveScore = new StoveScore();

    // ingredients get counted if completely fall through top
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag.ToString() == "Ingredient")
        {
            Destroy(target.gameObject);
            stoveScore.AddScore();
        }

        if (stoveScore.CheckIfFull())
        {
            backbutton.gameObject.SetActive(true);
        }
    }

    // bombs will get hit if you just touch them
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag.ToString() == "Bomb")
        {
            Destroy(target.gameObject);
            StartCoroutine(ShowText("YOU HIT A BOMB"));
            stoveScore.AddBombMultiplier();
        }
    }

    IEnumerator ShowText(string text)
    {
        errorTextBomb.text = text;
        yield return new WaitForSeconds(1);
        errorTextBomb.text = "";

    }
}



