using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dish", menuName ="Pantry System/Dish")]

public class DishObject : ItemObject
{   
    public int DishID;

    public int Score = 100;
    public List<IngredientObject> Recipe = new List<IngredientObject>();
    
    public void Awake() {
        Type = ItemType.Dish;
    }
}
