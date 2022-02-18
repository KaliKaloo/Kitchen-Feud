using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Assets/Orders")]
public class Order : ScriptableObject 
{
    public string orderID;
    public int orderNumber;
    public List<BaseFood> dishes = new List<BaseFood>();
    
    // public GameObject ticket;

  
}

