using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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
    [SerializeField] public GameObject startButton;
    [SerializeField] public GameObject team1Background;
    [SerializeField] public GameObject team2Background;
    [SerializeField] public GameObject minigameCanvas;
    public GameObject backButton;
    public GameObject topBar;
    public GameObject bottomBar;

    [SerializeField] public GameObject correctItem;
    public Appliance appliance;
    public Camera UICamera;

    public DishSO dishSO;
    private int chosenX;
    private int chosenY;
    BoxCollider2D boxCollider;

    private GameObject parentCanvas;

    //public float xBounds, yBound;
    public List<Sprite> newIngredients;
    public List<Sprite> bombs;

    StoveScore stoveScore = new StoveScore();
    StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();


    // Start is called before the first frame update
    void Start()
    {
        // boxCollider = bottomBar.GetComponent<BoxCollider2D>();
        // boxCollider.size = new Vector3(Screen.width, bottomBar.transform.lossyScale.y, bottomBar.transform.lossyScale.z);
        stoveMinigameCounter.StartGame();
        
        stoveScore.ResetValues();
        //Camera cam = Camera.main;
        //float height = 2f * UICamera.orthographicSize;
        //float width = height * UICamera.aspect;
        
        chosenY = (int)(2f * UICamera.orthographicSize);
        chosenX = (int)(chosenY  * UICamera.aspect);
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
        if (team1Background.activeSelf)
            parentCanvas = team1Background;
        else if (team2Background.activeSelf)
            parentCanvas = team2Background;

        stoveMinigameCounter.StartGame();
        stoveMinigameCounter.ResetCounter();
        
        topBar.SetActive(false);
        startButton.SetActive(false);
        startSmoke();

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
                new Vector3(Random.Range(0, chosenX), chosenY, 0), Quaternion.identity,
                parentCanvas.transform);
            obj.GetComponent<Image>().sprite = currentIngredient;

            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0);
           
            stoveMinigameCounter.MinusCounter();
            StartCoroutine(SpawnCorrectIngredient());
           
        } 
        
        else if (stoveMinigameCounter.GetCounter() == 0)
        {
            stoveMinigameCounter.EndGame();
            
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
                new Vector3(Random.Range(0, chosenX), chosenY, 0), Quaternion.identity,
                parentCanvas.transform);

            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0);
            obj.GetComponent<Image>().sprite = currentBomb;
            StartCoroutine(SpawnBombObject());
        }
    }


    private void startSmoke(){

        appliance.GetComponent<PhotonView>().RPC("syncSmoke", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID);
    }


}
