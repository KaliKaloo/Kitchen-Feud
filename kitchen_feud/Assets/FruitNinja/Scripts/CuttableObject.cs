using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableObject : MonoBehaviour
{
   public bool correctIngredient;
    public cutController CutController;

    public void OnCollisionEnter2D (Collision2D collision)
    {
        //identify if object is being cut by knife
        if (collision.gameObject.tag == "Cut") {
            Destroy (gameObject);
            CutController = GameObject.Find("CutController").transform.GetComponent<cutController>();
            
            if (!correctIngredient)
            {
                //cutController hey = GameObject.Find("CutController").transform.GetComponent<cutController>();
                CutController.Score -= 10;
                //GameObject.Find("CanvasController").transform.GetComponent<CanvasController>().ScoreText.Score -= 10;
            }
            else
            {
                //cutController hey = GameObject.Find("CutController").transform.GetComponent<cutController>();
                CutController.Ingredient += 1;
            }
        }
    }
}
