using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cutController : MonoBehaviour
{
    public ObjectSpawner IngredientSpawner;
    public ObjectSpawner BombSpawner;

    public DishSO dish;

    public List<Sprite> dishSprites = new List<Sprite>();
    public List<Sprite> bombSprites = new List<Sprite>();

    public GameObject scoreSystem;
    public Image Ingredient1;
    public Image Ingredient2;
    public Image Ingredient3;
    public Text scoretext;
    public Text numtext;

    public GameObject backButton;
    public GameObject StartButton;
    public int finalScore;

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
            numtext.text = num + "/5";
        }
    }

    void Start()
    { 
        dishSprites.Add(dish.recipe[0].img);
        dishSprites.Add(dish.recipe[1].img);
        dishSprites.Add(dish.recipe[2].img);
    }

    public void StartGame()
    {
        StartButton.SetActive(false);
        scoreSystem.SetActive(true);
        //call ingredient spawner with a list od ingredient sprites
        IngredientSpawner.StartSpawn(dishSprites);

        //call bomb spanwer with a list of "wrong" ingredient sprites
        BombSpawner.StartSpawn(bombSprites);

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

        finalScore = Score;
        Debug.Log(finalScore);
        GameEvents.current.assignPointsEventFunction();

    }
}
