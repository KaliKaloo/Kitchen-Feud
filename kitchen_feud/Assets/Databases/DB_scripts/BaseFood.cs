using UnityEngine;
public enum ItemType{
    Ingredient,
    Dish
}
public enum Location{
    None,
    Freezer,
    Fridge,

    Pantry
}

// [CreateAssetMenu(fileName = "New Item", menuName = "Kitchen/Item")]
public abstract class BaseFood : ScriptableObject
{
    new public string name = "New Item";
    public Location location;
    public GameObject Prefab;
    public ItemType Type;
    public Sprite img;

    public bool canPickUp = true;
    public int maxScore;
    public int finalScore;
}
