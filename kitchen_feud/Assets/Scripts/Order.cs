using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Assets/Orders")]
public class Order : ScriptableObject 
{
    public string orderID;
    public int orderNumber;
    public string orderMain;
    public string orderSide;
    public string orderDrink;
    
}
