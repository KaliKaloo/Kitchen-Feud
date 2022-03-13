using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SandwichController : MonoBehaviour
{
    public DishSO dish;
    // Start is called before the first frame update
    [SerializeField] private GameObject team1Background;
    [SerializeField] private GameObject team2Background;
    public GameObject StartButton;
    public GameObject backButton;
    public GameObject GameUI;
    
    //public Image Ingredient;
    //public SpawnIngredient spawnIngredient; 

    public Text scoreText;
    public SandwichMove SandwichMove;

    public bool moving = false;
    public int CountStopped;
    public int finalScore;
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

    void Update(){
        if (CountStopped == 4){
            StopGame();
        }
    }

    void Start()
    {
        //set bacground to team1
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
        {
            team1Background.SetActive(true);
            team2Background.SetActive(false);
        }
        //set bacground to team2
        else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
        {
            team1Background.SetActive(false);
            team2Background.SetActive(true);
        }
    }

    public void RestartGame()
    {
        StartButton.SetActive(true);
        backButton.SetActive(false);
        moving = false;
        Score = 0;
        finalScore = 0;
        CountStopped = 0;
        //SandwichMove.RestartMove();

    }

    public void StartGame()
    {
        StartButton.SetActive(false);
        GameUI.SetActive(true);
        Score = 0;
        moving = true;
        //SandwichMove.StartMoving();
       
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

    public void StopGame(){
        
        backButton.SetActive(true);
        finalScore = score;
        GameEvents.current.assignPointsEventFunction();
    }
}
