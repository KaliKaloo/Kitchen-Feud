using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cutController : MonoBehaviour
{
    public ObjectSpawner IngredientSpawner;
    public ObjectSpawner BombSpawner;

    public DishSO dish;

    public List<Sprite> newIngredients;
    public List<Sprite> bombSprites = new List<Sprite>();

    public GameObject instructions;
    public GameObject scoreSystem;
    public Image Ingredient1;
    public Image Ingredient2;
    public Image Ingredient3;
    public Text scoretext;
    public Text numtext;

    public GameObject backButton;
    public GameObject StartButton;
    public int finalScore;
    public AudioSource source;
    public float pitchMin, pitchMax, volumeMin, volumeMax;
    public AudioClip[] diffSounds;

    private int score = 100;
    private int num = 0;


    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoretext.text = "Score: " + score;
        }
    }

    public int Ingredient
    {
        get { return num; }
        set
        {
            num = value;
            numtext.text = num + "/15";
        }
    }

    public void RestartGame()
    {
        instructions.SetActive(true);
        StartButton.SetActive(true);
        backButton.SetActive(false);
        scoreSystem.SetActive(false);

        IngredientSpawner.StopSpawn();
        BombSpawner.StopSpawn();

        Score = 100;
        Ingredient = 0;
        finalScore = 0;
    }

    public List<Sprite> InstantiateList(List<IngredientSO> ingredients)
    {
        List<Sprite> dishSprites = new List<Sprite>();
        foreach (IngredientSO ingredient in ingredients)
        {
            dishSprites.Add(ingredient.img);
        }
        return dishSprites;
    }

    public void StartGame()
    {

        List<Sprite> dishSprites = InstantiateList(dish.recipe);
        newIngredients = new List<Sprite>(dishSprites);
        
        //instructions.SetActive(false);
        StartButton.SetActive(false);
        scoreSystem.SetActive(true);

        // start cooking animation
        playerAnimator.animator.SetBool("IsCooking", true);

        //call ingredient spawner with a list od ingredient sprites
        IngredientSpawner.StartSpawn(newIngredients);

        //call bomb spanwer with a list of "wrong" ingredient sprites
        BombSpawner.StartSpawn(bombSprites);
        
        scoretext.text = "Score: " + Score;
        Ingredient = 0;
        if (newIngredients.Count >= 1 && newIngredients[0])
            Ingredient1.sprite = newIngredients[0];
        if (newIngredients.Count >= 2 && newIngredients[1])
           Ingredient2.sprite = newIngredients[1];
        if (newIngredients.Count >= 3 && newIngredients[2])
            Ingredient3.sprite = newIngredients[2];

    }
 
    public void calculateScore()
    {
        scoreSystem.SetActive(false);
        
        backButton.SetActive(true);

        finalScore = Score;
        Debug.Log(finalScore);
        GameEvents.current.assignPointsEventFunction();
       
    }
}
