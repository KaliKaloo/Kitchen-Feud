using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    //database class has the order database inside it. you have to put the db here inside the inspector
    public OrderDatabase orders;
    public DishDatabase dishes;
    public IngredientDatabase ingredients;

    public IngredientSO in1;
    public IngredientSO in2;

    //make sure only one instance of our database is active
    private static Database orderInstance;
    private static Database dishInstance;
    private static Database ingredientInstance;

    //I dont know hpw this works lol
    private void Awake(){
        if(orderInstance == null)
        {
            orderInstance = this;

            //we want this to persist and not load a new instance of a db every scene
            //the gameObject the script is attached to is never destroyed during a scene transition 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("instance not null");
        }

        if (dishInstance == null)
        {
            dishInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (ingredientInstance == null)
        {
            ingredientInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //just to see if it works
    void Start()
    {
        GetDishFromIngredients(in1, in2);
    }

    //given 2 ingredientSO (or their IDs?) return the dish
    public static DishSO GetDishFromIngredients(IngredientSO ID_1, IngredientSO ID_2)
    {
        string id_1 = ID_1.ingredientID;
        string id_2 = ID_2.ingredientID;

        string madeDishID = "DI" + id_1 + id_2;
        
        foreach (DishSO madeDish in dishInstance.dishes.allDishes)
        {
            if (madeDish.dishID == madeDishID)
            {
                Debug.Log(madeDish.name);
                return madeDish;
            }
        }
        return null;
    }

    //the other ways around => given IngredientSO return the ID
    public static IngredientSO GetIngredientByID(string ID)
    {
        //return instance.orders.allOrders.FirstOrDefault(i => i.orderID == ID);
        foreach (IngredientSO ingredient in ingredientInstance.ingredients.allIngredients)
        {
            if (ingredient.ingredientID == ID)
                return ingredient;
        }
        return null;
    }

    //iterate over order database and return the one we want
    public static Order GetOrderByID(string ID)
    {
        //return instance.orders.allOrders.FirstOrDefault(i => i.orderID == ID);
        foreach (Order order in orderInstance.orders.allOrders)
        {
            if (order.orderID == ID)
                return order;
        }
        return null;
    }

    public static Order GetRandomOrder()
    {
        int m = Random.Range(0, orderInstance.orders.allOrders.Count);
        return orderInstance.orders.allOrders[m];

    }


}
