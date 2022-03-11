using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandwichController : MonoBehaviour
{
    public DishSO dish;
    // Start is called before the first frame update
    public GameObject StartButton;
    public GameObject GameUI;
    public Image Ingredient;

    public SpawnIngredient spawnIngredient; 

    public Text scoreText;
    //private 
    
    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = "" + score;
        }
    }

    void Start()
    {
        //set bacground to correct team
        //spawnIngredient.StartSpawn();
    }

    public void StartGame()
    {
        StartButton.SetActive(false);
        GameUI.SetActive(true);
        Score = 0;
       
        //StartSpawning();
        //ingredients spawn in the middle and start moving left and rigt
        //images of ingredients start appeairng in intervals on the left
        //player has to click on the correct object corresponding to the image 
        //it has to stop in the center.
    }

    // public void DisplayRandomIngredient()
    // {
    //     Ingredient.sprite = dish.recipe[0].img;
    // }
}
