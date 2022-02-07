
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Kitchen/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string ItemType = " ";
    public bool canPickUp = false;
    public bool canFry = false;
    public bool canCut = false;

}
