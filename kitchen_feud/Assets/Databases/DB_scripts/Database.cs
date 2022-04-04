using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    //database class has the order database inside it. you have to put the db here inside the inspector
    public OrderDatabase orders;
    public DishDatabase dishes;
    public IngredientDatabase ingredients;


    //make sure only one instance of our database is active
    private static Database orderInstance;
    private static Database dishInstance;
    private static Database ingredientInstance;

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


    public static DishSO GetDishFromIngredients(List<IngredientSO> ingredients)
    {
        string madeDishID = "DI";
        List<string> ingredientIDs_string = new List<string>();
        foreach( IngredientSO i in ingredients){
            ingredientIDs_string.Add(i.ingredientID);
        }
        List<int> ingredientIDs_int = ingredientIDs_string.ConvertAll(int.Parse);
        ingredientIDs_int.Sort();

        foreach( int i in ingredientIDs_int){
            madeDishID = madeDishID + i.ToString(); 
        }
        Debug.Log(madeDishID);
        foreach (DishSO madeDish in dishInstance.dishes.allDishes)
        {
            if (madeDish.dishID == madeDishID)
            {
                return madeDish;
            }
            else{
                continue;
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

    public static DishSO GetDishByID(string ID)
    {
        //return instance.orders.allOrders.FirstOrDefault(i => i.orderID == ID);
        foreach (DishSO dish in dishInstance.dishes.allDishes)
        {
            if (dish.dishID == ID)
                return dish;
        }
        return null;
    }


}