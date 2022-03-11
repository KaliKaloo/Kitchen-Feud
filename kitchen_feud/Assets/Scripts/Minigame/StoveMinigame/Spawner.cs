using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveMinigameCounter
{
    public static int counter;
    public static bool end;
    public static int collisionCounter;

    public void MinusCollisionCounter()
    {
        collisionCounter -= 1;
    }

    public int GetCollisionCounter()
    {
        return collisionCounter;
    }

    public void StartGame()
    {
        end = false;
        
    }
    public void EndGame()
    {
        end = true;
        //Spawner.backButton.SetActive(true);
    }

    public bool GetGameState()
    {
        
        return end;
        
    }

    public void ResetCounter()
    {
        counter = 15;
        collisionCounter = 15;
    }

    public void MinusCounter()
    {
        counter -= 1;
    }

    public int GetCounter()
    {
        //Debug.Log(counter);
        return counter;

    }
}


    public class Spawner : MonoBehaviour
{

    [SerializeField] public GameObject[] ingredients;
    [SerializeField] public GameObject bomb;
    [SerializeField] public GameObject parentCanvas;
    [SerializeField] public GameObject startButton;
    public GameObject backButton;

    [SerializeField] public GameObject correctItem;

    public DishSO dishSO;
    private int chosenX;
    private int chosenY;

    //public float xBounds, yBound;
    public List<Sprite> newIngredients;
    public List<Sprite> bombs;

    StoveScore stoveScore = new StoveScore();
    StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();


    // Start is called before the first frame update
    void Start()
    {
        stoveMinigameCounter.StartGame();
        stoveScore.ResetValues();
        chosenX = Screen.width;
        chosenY = Screen.height;
        backButton.SetActive(false);
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
        stoveMinigameCounter.StartGame();
        stoveMinigameCounter.ResetCounter();
        startButton.SetActive(false);
        
        List<Sprite> dishSprites = InstantiateList(dishSO.recipe);
        stoveScore.SetAmountInitialIngredients(dishSprites.Count);
        newIngredients = new List<Sprite>(dishSprites);

        StartCoroutine(SpawnCorrectIngredient());
        StartCoroutine(SpawnBombObject());
    }


    public void StopGame(){
        stoveMinigameCounter.EndGame();

    }

    IEnumerator SpawnCorrectIngredient()
    {
       
        yield return new WaitForSeconds(Random.Range(0.5f, 1));

        int randomIngredient = Random.Range(0, newIngredients.Count);

        if (stoveMinigameCounter.GetCounter() > 0)
        {
            Sprite currentIngredient = newIngredients[randomIngredient];
            GameObject obj = Instantiate(correctItem,
                new Vector2(Random.Range(0, chosenX), chosenY), Quaternion.identity,
                parentCanvas.transform);
            obj.GetComponent<Image>().sprite = currentIngredient;

            stoveMinigameCounter.MinusCounter();
            StartCoroutine(SpawnCorrectIngredient());
           
        } 
        
        else if (stoveMinigameCounter.GetCounter() == 0)
        {
            stoveMinigameCounter.EndGame();
            Debug.Log("ended spawning");
            
        }
    }

    IEnumerator SpawnBombObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));

        int randomBomb = Random.Range(0, bombs.Count);

        if (stoveMinigameCounter.GetCounter() > 0)
        {
            Sprite currentBomb = bombs[randomBomb];
            GameObject obj = Instantiate(bomb,
                new Vector2(Random.Range(0, chosenX), chosenY), Quaternion.identity,
                parentCanvas.transform);
            obj.GetComponent<Image>().sprite = currentBomb;
            StartCoroutine(SpawnBombObject());
        }
    }
}