using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ingredient", menuName ="Pantry System/Ingredient")]
public class IngredientObject : ItemObject
{
    public int IngredientID;
    public void Awake() {
        Type = ItemType.Ingredient;
    }
}
