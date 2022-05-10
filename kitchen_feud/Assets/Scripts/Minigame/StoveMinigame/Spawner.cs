using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

// spawns all ingredients in the stove minigame
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

    // put all sprites in list based on parsed dish
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
        // set correct background based on kitchen
        if (team1Background.activeSelf)
            parentCanvas = team1Background;
        else if (team2Background.activeSelf)
            parentCanvas = team2Background;

        StoveMinigameCounter.ResetCounters();
        stoveMinigameCounter.StartGame();
        startButton.SetActive(false);

        if (PhotonNetwork.IsConnected)
            startSmoke();


        List<Sprite> dishSprites = InstantiateList(dishSO.recipe);
        stoveScore.SetAmountInitialIngredients(dishSprites.Count);
        newIngredients = new List<Sprite>(dishSprites);

        // start cooking animation
        if (playerAnimator.animator != null)
            playerAnimator.animator.SetBool("IsCooking", true);

        // start spawning items falling from ceiling
        StartCoroutine(SpawnCorrectIngredient());
        StartCoroutine(SpawnBombObject());
    }

    IEnumerator SpawnCorrectIngredient()
    {
        // spawns randomly in a small interval
        yield return new WaitForSeconds(Random.Range(0.5f, 1));

        int randomIngredient = Random.Range(0, newIngredients.Count);

        // only keep spawning if haven't caught max amount of ingredients
        if (StoveMinigameCounter.collisionCounter < StoveMinigameCounter.amount)
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
        
        else if (StoveMinigameCounter.droppedCounter == StoveMinigameCounter.amount)
        {
            stoveMinigameCounter.EndGame();
            
        }
    }

    IEnumerator SpawnBombObject()
    {
        // spawns bombs slightly infrequently compared to other ingredients
        yield return new WaitForSeconds(Random.Range(1, 2));
        int randomBomb = Random.Range(0, bombs.Count);

        // only keep spawning if haven't caught max amount of ingredients
        if (StoveMinigameCounter.collisionCounter < StoveMinigameCounter.amount)
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

    // start smoke particles on stove appliance
    private void startSmoke(){
        appliance.GetComponent<PhotonView>().RPC("syncSmoke", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID);
    }

}
