using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ingredient", menuName ="Pantry/Ingredient")]
public class IngredientObject : ItemObject
{
    public void Awake() {
        Type = ItemType.Ingredient;
    }
}
