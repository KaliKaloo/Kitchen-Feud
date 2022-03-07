using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient Database", menuName = "Assets/Databases/Ingredient Database")]
public class IngredientDatabase : ScriptableObject
{
    public List<IngredientSO> allIngredients;

}

