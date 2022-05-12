using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class SandwichControllerTests
{
    SandwichController sandwichController;
    List<IngredientSO> ingredients;
    IngredientSO ingredient1;
    IngredientSO ingredient2;
    IngredientSO ingredient3;



    [OneTimeSetUp]
    public void oneTimeSetUp(){
        GameObject obj = new GameObject();
        sandwichController = obj.AddComponent<SandwichController>();
        sandwichController.scoreText = obj.AddComponent<Text>();
        sandwichController.StartButton = new GameObject();
        sandwichController.backButton = new GameObject();
        sandwichController.instructions = new GameObject();

        sandwichController.SandwichSpawner = obj.AddComponent<SandwichSpawner>();
        sandwichController.LayerSpawn = obj.AddComponent<LayerSpawn>();

        sandwichController.GameUI = new GameObject();

        ingredients = new List<IngredientSO>();
        ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient1.ingredientID = "ingr 1";
        ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient2.ingredientID = "ingr 2";
        ingredient3 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient3.ingredientID = "ingr 3";

    }

    [SetUp]
    public void SetUp(){
        ingredients = new List<IngredientSO>();

    }


    [TearDown]
    public void tearDown(){
        ingredients.Clear();
    }



    [Test]
    public void InitSandwichController()
    {
        Assert.IsFalse(sandwichController.moving);
        Assert.AreEqual(0, sandwichController.Score);
    }

    [Test]
    public void ScoreGetterSetter()
    {
        sandwichController.Score = 5;
        int score = sandwichController.Score;
        Assert.AreEqual(5, score);
        Assert.AreEqual("5", sandwichController.scoreText.text);

    }

    [Test]
    public void RestartGame()
    {

        sandwichController.Score = 30;
        sandwichController.finalScore = 30;
        sandwichController.CountStopped = 2;
        sandwichController.currentIndex = 7;
        sandwichController.moving = true;

        sandwichController.RestartGame();
        Assert.IsFalse(sandwichController.moving);
        Assert.AreEqual(0, sandwichController.Score);
        Assert.AreEqual(0, sandwichController.finalScore);
        Assert.AreEqual(0, sandwichController.CountStopped);
       
        Assert.AreEqual(0, sandwichController.sandwichIngredients.Count);
        Assert.AreEqual(0, sandwichController.idList.Count);
        Assert.AreEqual(0, sandwichController.objectPool.Count);

        Assert.AreEqual(0, sandwichController.currentIndex);
        Assert.AreEqual("", sandwichController.currentActiveID);

    }



    [Test]
    public void InstantiateListEmpty()
    {
        List<string> ingredientList = sandwichController.InstantiateList(ingredients);
        Assert.AreEqual(new List<string>(){}, ingredientList);
    }

    [Test]
    public void InstantiateListOne()
    {
        ingredients.Add(ingredient1);
        List<string> ingredientList = sandwichController.InstantiateList(ingredients);
        Assert.AreEqual(new List<string>(){"ingr 1"}, ingredientList);
    }

    [Test]
    public void InstantiateListMultiple()
    {
        ingredients.Add(ingredient1);
        ingredients.Add(ingredient2);
        ingredients.Add(ingredient3);
        List<string> ingredientList = sandwichController.InstantiateList(ingredients);
        Assert.AreEqual(new List<string>(){"ingr 1", "ingr 2", "ingr 3"}, ingredientList);
    }


    [Test]
    public void StopGame()
    {
        sandwichController.StopGame();
        Assert.IsTrue(sandwichController.backButton.activeSelf);
        Assert.AreEqual(sandwichController.Score, sandwichController.finalScore);
    }


    
}
