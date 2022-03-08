using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] public GameObject[] ingredients;
    [SerializeField] public GameObject bomb;
    [SerializeField] public GameObject parentCanvas;

    public float xBounds, yBound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRandomGameObject());
    }

    IEnumerator SpawnRandomGameObject()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));

        int randomIngredient = Random.Range(0, 1);

        if (Random.value <= 0.5f)
        {
            Instantiate(ingredients[randomIngredient],
                new Vector2(Random.Range(0, xBounds), yBound), Quaternion.identity,
                parentCanvas.transform);
        } else
        {
            Instantiate(bomb,
                new Vector2(Random.Range(0, xBounds), yBound), Quaternion.identity,
                parentCanvas.transform);
        }
        StartCoroutine(SpawnRandomGameObject());
    }
}
