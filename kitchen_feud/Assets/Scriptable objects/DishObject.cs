using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dish", menuName ="Pantry/Dish")]

public class DishObjects : ItemObject
{   
    public List<IngredientObject> Recipe = new List<IngredientObject>();
    public int Score = 100;
    
    public void Awake() {
        Type = ItemType.Dish;
    }
}
