using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    //database class has the order database inside it. you have to put the db here inside the inspector
    public OrderDatabase orders;
    //make sure only one instance of our database is active
    private static Database instance;
   
    
    private void Awake(){
        if(instance == null)
        {
            instance = this;

            //we want this to persist and not load a new instance of a db every scene
            //the gameObject the script is attached to is never destroyed during a scene transition 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("instance not null");
        }
    }

    //iterate over order database and return the one we want
    public static Order GetOrderByID(string ID)
    {
        //return instance.orders.allOrders.FirstOrDefault(i => i.orderID == ID);
        foreach (Order order in instance.orders.allOrders)
        {
            if (order.orderID == ID)
                return order;
        }
        return null;
    }

    public static Order GetRandomOrder()
    {
        int m = Random.Range(0, instance.orders.allOrders.Count);
        return instance.orders.allOrders[m];

    }


}
