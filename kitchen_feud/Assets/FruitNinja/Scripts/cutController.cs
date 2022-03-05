using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cutController : MonoBehaviour
{
    public ObjectSpawner IngredientSpawner;
    public ObjectSpawner BombSpawner;

    public GameObject scoreSystem;
    public DishSO dish;

    public Image Ingredient1;
    public Image Ingredient2;
    public Image Ingredient3;
    public Text scoretext;
    public Text numtext;

    public GameObject backButton;

    public List<Sprite> dishSprites = new List<Sprite>();

    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoretext.text = "Score: " + score;
        }
    }

    private int num = 0;
    public int Ingredient
    {
        get { return num; }
        set
        {
            num = value;
            numtext.text = num + "/15";
        }
    }

    void Start()
    {
        Score = dish.maxScore;
        Ingredient = 0;

        Ingredient1.sprite = dish.recipe[0].img;
        Ingredient2.sprite = dish.recipe[1].img;
        Ingredient3.sprite = dish.recipe[2].img;

    }

    public void calculateScore()
    {
        scoreSystem.SetActive(false);
        Debug.Log("Game Over");
        backButton.SetActive(true);

        dish.finalScore = score;
        Debug.Log(dish.finalScore);
        backButton.SetActive(true);
    }
}
