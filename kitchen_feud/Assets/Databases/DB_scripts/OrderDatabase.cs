using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order Database", menuName = "Assets/Databases/Order Database")]
public class OrderDatabase : ScriptableObject
{
    public List<Order> allOrders;
    
}
