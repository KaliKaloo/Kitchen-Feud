using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class SandwichSpawnerTests
{

    [Test]
    public void spawnObject()
    {

        GameObject obj = new GameObject();
        SandwichSpawner sandwichSpawner = obj.AddComponent<SandwichSpawner>();
        GameObject prefabObj = new GameObject();
        prefabObj.AddComponent<Image>();
        prefabObj.AddComponent<SandwichID>();
        sandwichSpawner.prefabToSpawn = prefabObj;
        IngredientSO ingredient =  ScriptableObject.CreateInstance<IngredientSO>();
        Sprite sprite1 = Sprite.Create(new Texture2D(3, 3), new Rect(0.0f, 0.0f, 1, 1), new Vector2(0.5f, 0.5f), 5.0f);
        ingredient.img = sprite1;
        ingredient.ingredientID = "my ID";
        GameObject spawnedObj = sandwichSpawner.spawnObject(ingredient);
        Assert.AreEqual(sprite1, spawnedObj.GetComponent<Image>().sprite);
        Assert.AreEqual("my ID", spawnedObj.GetComponent<SandwichID>().Id);

    }

  
}
