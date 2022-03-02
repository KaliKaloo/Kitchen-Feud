using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    public GameObject prefabToSpawn;

    public float spawnInterval;
    public float objectMinX;
    public float objectMaxX;
    public float objectY;

    public Sprite[] objectSprites;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("spawnObject", this.spawnInterval, this.spawnInterval);
    }
    private void spawnObject()
    {
        GameObject newObject = Instantiate(this.prefabToSpawn);
        newObject.transform.position = new Vector2(Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
        
        //instantiate a random sprite
        Sprite objectSprite = objectSprites[Random.Range(0, this.objectSprites.Length)];
        newObject.GetComponent<SpriteRenderer>().sprite = objectSprite;
    }
}