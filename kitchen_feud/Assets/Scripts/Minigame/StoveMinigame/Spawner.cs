using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Spawner : MonoBehaviour
{

    [SerializeField] public GameObject[] ingredients;
    [SerializeField] public GameObject bomb;
    [SerializeField] public GameObject startButton;
    [SerializeField] public GameObject team1Background;
    [SerializeField] public GameObject team2Background;
    [SerializeField] public GameObject minigameCanvas;
    public GameObject backButton;
    public GameObject instructions;
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


    void Start()
    {
        stoveMinigameCounter.StartGame();
        
        stoveScore.ResetValues();
        
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
        StoveMinigameCounter.ResetCounters();
        
        instructions.SetActive(false);
        startButton.SetActive(false);
        if (PhotonNetwork.IsConnected)
            startSmoke();
            //Sparks -------------------------------------
            //startSparks();
            //--------------------------------------------

        List<Sprite> dishSprites = InstantiateList(dishSO.recipe);
        stoveScore.SetAmountInitialIngredients(dishSprites.Count);
        newIngredients = new List<Sprite>(dishSprites);

        // start cooking animation
        if (playerAnimator.animator != null)
            playerAnimator.animator.SetBool("IsCooking", true);

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

        if (StoveMinigameCounter.droppedCounter < 20)
        {
            Sprite currentIngredient = newIngredients[randomIngredient];
            GameObject obj = Instantiate(correctItem,
                new Vector3(Random.Range(0, chosenX), chosenY, 0), Quaternion.identity,
                parentCanvas.transform);
            obj.GetComponent<Image>().sprite = currentIngredient;

            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0);
           
            stoveMinigameCounter.AddDroppedCounter();
            StartCoroutine(SpawnCorrectIngredient());
           
        } 
        
        else if (StoveMinigameCounter.droppedCounter == 20)
        {
            stoveMinigameCounter.EndGame();
            
        }
    }

    IEnumerator SpawnBombObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));

        int randomBomb = Random.Range(0, bombs.Count);

        if (StoveMinigameCounter.droppedCounter < 20)
        {
            Sprite currentBomb = bombs[randomBomb];
            GameObject obj = Instantiate(bomb,
                new Vector3(Random.Range(0, chosenX), chosenY, 0), Quaternion.identity,
                parentCanvas.transform);

            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0);
            obj.GetComponent<Image>().sprite = currentBomb;
            stoveMinigameCounter.AddDroppedCounter();
            StartCoroutine(SpawnBombObject());
        }
    }


    private void startSmoke(){

        appliance.GetComponent<PhotonView>().RPC("syncSmoke", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID);
    }

    //SPARKS ------------------------------------
    /*private void startSparks(){

        appliance.GetComponent<PhotonView>().RPC("syncSparks", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID);
    }*/
    //-------------------------------------------


}
