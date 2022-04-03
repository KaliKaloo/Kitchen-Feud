using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class CutControllerTests
{

    cutController cutController;


    [OneTimeSetUp]
    public void setUp(){
        GameObject obj = new GameObject();
        cutController = obj.AddComponent<cutController>();
        cutController.scoretext = obj.AddComponent<Text>();
        GameObject newObj = new GameObject();
        cutController.numtext = newObj.AddComponent<Text>();
        cutController.instructions = new GameObject();
        cutController.StartButton = new GameObject();
        cutController.backButton = new GameObject();
        cutController.scoreSystem = new GameObject();
        cutController.IngredientSpawner = obj.AddComponent<ObjectSpawner>();
        cutController.BombSpawner = obj.AddComponent<ObjectSpawner>();
        cutController.scoreSystem = new GameObject();
        GameEvents gameEvents = obj.AddComponent<GameEvents >();
        GameEvents.current = gameEvents;

      

    }


    [Test]
    public void ScoreGetterSetter()
    {
        cutController.Score = 5;
        int score = cutController.Score;
        Assert.AreEqual(5, score);
        Assert.AreEqual("Score: 5", cutController.scoretext.text);

    }


    [Test]
    public void IngredientGetterSetter()
    {
        cutController.Ingredient = 10;
        int ingredient = cutController.Ingredient;
        Assert.AreEqual(10, ingredient);
        Assert.AreEqual("10/15", cutController.numtext.text);
    }


    [Test]
    public void RestartGame()
    {

        cutController.Score = 20;
        cutController.finalScore = 30;

        cutController.RestartGame();
        Assert.IsTrue(cutController.instructions.activeSelf);
        Assert.IsTrue(cutController.StartButton.activeSelf);
        Assert.IsFalse(cutController.backButton.activeSelf);
        Assert.IsFalse(cutController.scoreSystem.activeSelf);
        Assert.AreEqual(100, cutController.Score);
        Assert.AreEqual(0, cutController.Ingredient);
        Assert.AreEqual(0, cutController.finalScore);

    }


    [Test]
    public void calculateScore()
    {
        cutController.calculateScore();
        Assert.IsTrue(cutController.backButton.activeSelf);
        Assert.IsFalse(cutController.scoreSystem.activeSelf);
        Assert.AreEqual(cutController.Score, cutController.finalScore);

    }

    [Test]
    public void InstantiateEmptyList()
    {
        List<IngredientSO> ingredients = new List<IngredientSO>(); 

        List<Sprite> dishSprites = cutController.InstantiateList(ingredients);
        Assert.AreEqual(0, dishSprites.Count);

    }

    [Test]
    public void Instantiate3List()
    {
        List<IngredientSO> ingredients = new List<IngredientSO>(); 
        IngredientSO ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        Sprite sprite1 = Sprite.Create(new Texture2D(3, 3), new Rect(0.0f, 0.0f, 1, 1), new Vector2(0.5f, 0.5f), 5.0f);
        ingredient1.img = sprite1;
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        IngredientSO ingredient3 = ScriptableObject.CreateInstance<IngredientSO>();

        ingredients.Add(ingredient1);
        ingredients.Add(ingredient2);
        ingredients.Add(ingredient3);

        List<Sprite> dishSprites = cutController.InstantiateList(ingredients);

        Assert.AreEqual(3, dishSprites.Count);
        Assert.AreEqual(sprite1, dishSprites[0]);


    }

}
