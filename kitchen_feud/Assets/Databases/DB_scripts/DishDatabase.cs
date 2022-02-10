using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dish Database", menuName = "Assets/Databases/Dish Database")]
public class DishDatabase : ScriptableObject
{
    public List<DishSO> allDishes;

}
