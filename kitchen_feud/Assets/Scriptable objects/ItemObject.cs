
using UnityEngine;

public enum ItemType{
    Ingredient,
    Dish
}

// [CreateAssetMenu(fileName = "New Item", menuName = "Kitchen/Item")]
public abstract class ItemObject : ScriptableObject
{
    new public string name = "New Item";
    public GameObject Prefab;
    public ItemType Type;
    public bool canPickUp = true;
}
