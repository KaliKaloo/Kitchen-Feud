using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;


public class SpawnerTests
{
    Spawner spawner;
    List<IngredientSO> ingredients;


    [OneTimeSetUp]
    public void oneTimeSetUp()
    {
        GameObject obj = new GameObject();
        spawner = obj.AddComponent<Spawner>();
        spawner.team1Background = new GameObject();
        spawner.team2Background = new GameObject();
        spawner.minigameCanvas = new GameObject();
        spawner.topBar = new GameObject();
        spawner.startButton = new GameObject();
        spawner.appliance = obj.AddComponent<Appliance>();
        spawner.dishSO = ScriptableObject.CreateInstance<DishSO>();

    }


    [SetUp]
    public void SetUp(){
        ingredients = new List<IngredientSO>();

    }

    [Test]
    public void StartGameInstantiateEmpty()
    {
        spawner.StartGame();

        Assert.IsFalse(spawner.topBar.activeSelf);
        Assert.IsFalse(spawner.startButton.activeSelf);
        Assert.AreEqual(0, spawner.newIngredients.Count);
        Assert.AreEqual(new List<Sprite>(){}, spawner.newIngredients);
    }


    public void StartGame()
    {   
        IngredientSO ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        Sprite sprite1 = Sprite.Create(new Texture2D(3, 3), new Rect(0.0f, 0.0f, 1, 1), new Vector2(0.5f, 0.5f), 5.0f);
        ingredient1.img = sprite1;
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        spawner.dishSO.recipe = new List<IngredientSO>(){ingredient1, ingredient2};

        spawner.StartGame();

        Assert.IsFalse(spawner.topBar.activeSelf);
        Assert.IsFalse(spawner.startButton.activeSelf);
        Assert.AreEqual(2, spawner.newIngredients.Count);
        Assert.AreEqual(ingredient1, spawner.newIngredients);

    }

    [Test]
    public void InstantiateEmptyList()
    {
        List<Sprite> dishSprites = spawner.InstantiateList(ingredients);
        Assert.AreEqual(0, dishSprites.Count);
        Assert.AreEqual(new List<Sprite>(){}, dishSprites);
    }


    
    [Test]
    public void Instantiate3List()
    {

        IngredientSO ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        Sprite sprite1 = Sprite.Create(new Texture2D(3, 3), new Rect(0.0f, 0.0f, 1, 1), new Vector2(0.5f, 0.5f), 5.0f);
        ingredient1.img = sprite1;
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        IngredientSO ingredient3 = ScriptableObject.CreateInstance<IngredientSO>();

        ingredients.Add(ingredient1);
        ingredients.Add(ingredient2);
        ingredients.Add(ingredient3);

        List<Sprite> dishSprites = spawner.InstantiateList(ingredients);

        Assert.AreEqual(3, dishSprites.Count);
        Assert.AreEqual(sprite1, dishSprites[0]);
        ingredients.Add(ingredient1);
    }
    
    
  
}
