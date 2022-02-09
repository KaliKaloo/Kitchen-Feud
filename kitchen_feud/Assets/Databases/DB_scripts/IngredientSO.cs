using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum Location{
//     Freezer,
//     Fridge,

//     Pantry
// }

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Assets/Ingredient")]
public class IngredientSO : BaseFood
{
    
    public string ingredientID;
    // public Location location;

    public bool canFry = false;
    public bool canCut = false;

}

