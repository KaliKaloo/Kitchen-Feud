using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] public GameObject[] ingredients;
    [SerializeField] public GameObject bomb;
    [SerializeField] public GameObject parentCanvas;

    public float xBounds, yBound;
    public List<GameObject> newIngredients;
    StoveScore stoveScore = new StoveScore();


    // Start is called before the first frame update
    void Start()
    {
        stoveScore.SetAmountInitialIngredients(ingredients.Length);
        newIngredients = new List<GameObject>(ingredients);
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
                GameObject currentIngredient = newIngredients[randomIngredient];
                Instantiate(currentIngredient,
                    new Vector2(Random.Range(100, xBounds), yBound), Quaternion.identity,
                    parentCanvas.transform);
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
