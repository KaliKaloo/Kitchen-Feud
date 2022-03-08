using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStoveMinigame : MonoBehaviour
{
    //public DishSO dish;

    //public List<Sprite> dishSprites = new List<Sprite>();
    //public List<Sprite> bombSprites = new List<Sprite>();

    //public Image Ingredient1;
    //public Image Ingredient2;
    //public Image Ingredient3;
    [SerializeField] public Text errorTextBomb;
    [SerializeField] public Text errorTextIngredient;
    [SerializeField] public GameObject stoveCanvas;



    StoveScore stoveScore = new StoveScore();



    void Start()
    {
        //dishSprites.Add(dish.recipe[0].img);
        //dishSprites.Add(dish.recipe[1].img);
        //dishSprites.Add(dish.recipe[2].img);

        errorTextBomb.text = "";
        errorTextIngredient.text = "";

        // do this when attached to game
        // stoveScore.SetAmountInitialIngredients(dishSprites.Count)

    }

    private void Update()
    {
        if (stoveCanvas.activeSelf)
        {
            if (stoveScore.CheckIfFull())
            {
                Debug.Log(stoveScore.FinalMultipier().ToString());
                stoveCanvas.SetActive(false);
            }
        }
    }

}
