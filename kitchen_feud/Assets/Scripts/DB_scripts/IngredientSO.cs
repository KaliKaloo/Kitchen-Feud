using UnityEngine;

public enum Location{
    Freezer,
    Fridge,

    Pantry
}

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Assets/Ingredient")]
public class IngredientSO : ScriptableObject
{
    
    public string ingredientID;
    public string ingredientName;
    public Location location;
    public bool canPickUp = false;
    public bool canFry = false;
    public bool canCut = false;
}

