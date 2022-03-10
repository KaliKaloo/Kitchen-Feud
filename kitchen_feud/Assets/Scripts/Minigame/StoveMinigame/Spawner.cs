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
        return counter;
    }
}


    public class Spawner : MonoBehaviour
{

    [SerializeField] public GameObject[] ingredients;
    [SerializeField] public GameObject bomb;
    [SerializeField] public GameObject parentCanvas;
    [SerializeField] public GameObject startButton;

    [SerializeField] public GameObject correctItem;

    public DishSO dishSO;

    public float xBounds, yBound;
    public List<Sprite> newIngredients;
    StoveScore stoveScore = new StoveScore();
    StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();


    // Start is called before the first frame update
    void Start()
    {
        stoveMinigameCounter.StartGame();
        stoveScore.ResetValues();
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

    IEnumerator SpawnCorrectIngredient()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1));

        int randomIngredient = Random.Range(0, newIngredients.Count-1);

        if (stoveMinigameCounter.GetCounter() > 0)
        {
            Sprite currentIngredient = newIngredients[randomIngredient];
            GameObject obj = Instantiate(correctItem,
                new Vector2(Random.Range(100, xBounds), yBound), Quaternion.identity,
                parentCanvas.transform);
            obj.GetComponent<Image>().sprite = currentIngredient;

            stoveMinigameCounter.MinusCounter();
            StartCoroutine(SpawnCorrectIngredient());
        } else
        {
            stoveMinigameCounter.EndGame();
        }
    }

    IEnumerator SpawnBombObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));

        if (stoveMinigameCounter.GetCounter() > 0)
        {
            Instantiate(bomb,
                new Vector2(Random.Range(0, xBounds), yBound), Quaternion.identity,
                parentCanvas.transform);
            StartCoroutine(SpawnBombObject());
        }
    }
}
