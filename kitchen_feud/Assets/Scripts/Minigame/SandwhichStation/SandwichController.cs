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
    
    //public SpawnIngredient spawnIngredient; 
    public List<IngredientSO> sandwichIngredients = new List<IngredientSO>();
    public List<GameObject>  objectPool = new List<GameObject>();
    public Text scoreText;
    
    public SandwichSpawner SandwichSpawner;
    public LayerSpawn LayerSpawn;
    public List<string> idList = new List<string>();

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
        sandwichIngredients.Clear();
    }


    public void StartGame()
    {
        RestartGame();
        StartButton.SetActive(false);
        GameUI.SetActive(true);
        Score = 0;
        moving = true;
       
       
        //make a copy of list of ingredients and add bread again at the end
        sandwichIngredients = new List<IngredientSO>(dish.recipe);
        sandwichIngredients.Add(dish.recipe[0]);
        
        List<string> ingredientIDs = InstantiateList(sandwichIngredients);
        idList = new List<string>(ingredientIDs);

        LayerSpawn.StartSpawn(idList);

        objectSpawn(sandwichIngredients);
    }

     public List<string> InstantiateList(List<IngredientSO> ingredients)
    {
        List<string> ingredientIDs = new List<string>();
        foreach (IngredientSO ingredient in ingredients)
        {
            ingredientIDs.Add(ingredient.ingredientID);
        }
        return ingredientIDs;
    }

    public void objectSpawn(List<IngredientSO> sandwichIngredients){   
        foreach (IngredientSO i in sandwichIngredients) {

            GameObject obj =  SandwichSpawner.spawnObject(i);
            obj.SetActive(false);
            objectPool.Add(obj);
        }

        InvokeRepeating("NewRandomObject", 1, 1);
    }

    public int currentIndex;
    public string currentActiveID;
    public void NewRandomObject()
     {
         int newIndex = Random.Range(0, objectPool.Count);
         // Deactivate old gameobject
         objectPool[currentIndex].SetActive(false);
         // Activate new gameobject
         currentIndex = newIndex;
         objectPool[currentIndex].SetActive(true);
         currentActiveID = objectPool[currentIndex].GetComponent<SandwichID>().Id;
        Debug.Log(currentActiveID);
     }

    public bool checkStoppedID(string objectID){
        if (currentActiveID == objectID){
            return true;
        } 
        return false;

    }


    public void StopGame(){
        
        backButton.SetActive(true);
        finalScore = score;
        GameEvents.current.assignPointsEventFunction();
    }
}
