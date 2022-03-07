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
                     CutController.Score -= 10;
            }
            else
            {
                CutController.Ingredient += 1;
                
                if (CutController.Ingredient == 5)
                {
                    CutController.calculateScore();
                }
            }
        }
    }
}
