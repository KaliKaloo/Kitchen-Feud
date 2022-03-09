using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    // Start is called before the first frame update
    void Start()
    {
        stoveScore.SetAmountInitialIngredients(5);
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
        startButton.SetActive(false);
        List<Sprite> dishSprites = InstantiateList(dishSO.recipe);
        stoveScore.SetAmountInitialIngredients(dishSprites.Count);
        newIngredients = new List<Sprite>(dishSprites);

        StartCoroutine(SpawnRandomGameObject());
    }

    IEnumerator SpawnRandomGameObject()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1));

        int randomIngredient = Random.Range(0, 1);

        if (newIngredients.Count > 0)
        {
            if (Random.value <= 0.2f)
            {
                Sprite currentIngredient = newIngredients[randomIngredient];
                GameObject obj = Instantiate(correctItem,
                    new Vector2(Random.Range(100, xBounds), yBound), Quaternion.identity,
                    parentCanvas.transform);
                obj.GetComponent<Image>().sprite = currentIngredient;

                newIngredients.Remove(currentIngredient);
            }
            else
            {
                Instantiate(bomb,
                    new Vector2(Random.Range(0, xBounds), yBound), Quaternion.identity,
                    parentCanvas.transform);
            }
            StartCoroutine(SpawnRandomGameObject());
        }
    }
}
