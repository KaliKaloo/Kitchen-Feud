using UnityEngine;

[CreateAssetMenu(fileName = "New Dish", menuName = "Assets/Dish")]
public class DishSO : ScriptableObject
{
    //dishID is made by combining the indgredientIDs of ingredients take make the dish
    public string dishID;
    public string dishName;
    public int maxScore;
    public int cookingTime;
    public string toCook;
    public int finalScore;
    public ScriptableObject ingredient1;
    public ScriptableObject ingredient2;
    public ScriptableObject ingredient3;

    // public GameObject ticket;


}

