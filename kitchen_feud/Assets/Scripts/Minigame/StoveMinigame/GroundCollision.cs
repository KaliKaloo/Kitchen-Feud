using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundCollision : MonoBehaviour
{
    StoveScore stoveScore = new StoveScore();
    [SerializeField] public Text errorTextIngredient;
    [SerializeField] public GameObject backbutton;



    void OnTriggerEnter2D(Collider2D target)
    {
        Destroy(target.gameObject);
        if (target.tag.ToString() == "Ingredient")
        {
            StartCoroutine(ShowText("You let an ingredient fall!"));
            stoveScore.MinusIngredient();
        }

        if (stoveScore.CheckIfFull())
        {
            backbutton.gameObject.SetActive(true);
        }
    }

    IEnumerator ShowText(string text)
    {
        errorTextIngredient.text = text;
        yield return new WaitForSeconds(2);
        errorTextIngredient.text = "";

    }

}
