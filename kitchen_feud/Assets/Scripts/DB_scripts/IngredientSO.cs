using UnityEngine;

public enum ItemType{
    Freezer,
    Fridge,

    Pantry
}

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Assets/Ingredient")]
public class IngredientSO : ScriptableObject
{
    
    public string ingredientID;
    public string ingredientName;
    public ItemType type;
    public bool canPickUp = false;
    public bool canFry = false;
    public bool canCut = false;
}

