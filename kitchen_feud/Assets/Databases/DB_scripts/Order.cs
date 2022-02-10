using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Assets/Orders")]
public class Order : ScriptableObject 
{
    public string orderID;
    public int orderNumber;
    public List<DishSO> dishes = new List<DishSO>();
    
    // public GameObject ticket;

  
}

