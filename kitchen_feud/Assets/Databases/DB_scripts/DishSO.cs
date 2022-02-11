using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dish", menuName = "Assets/Dish")]
public class DishSO : BaseFood
{
    //dishID is made by combining the indgredientIDs of ingredients take make the dish
    public string dishID;
    public string toCook;
    public int cookingTime;

    // public int maxScore;
    // public int finalScore;
    public List<IngredientSO> recipe = new List<IngredientSO>();

    // public GameObject ticket;

}

