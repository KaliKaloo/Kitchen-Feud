using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;

    public float spawnInterval;
    public float objectMinX;
    public float objectMaxX;
    public float objectY;

    private cutController CutController;
    public List<Sprite> dishSprites = new List<Sprite>();

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("spawnObject", spawnInterval, spawnInterval);
        CutController = GameObject.Find("CutController").transform.GetComponent<cutController>();
        DishSO dd = CutController.dish;

        dishSprites.Add(dd.recipe[0].img);
        dishSprites.Add(dd.recipe[1].img);
        dishSprites.Add(dd.recipe[2].img);
    }
    private void spawnObject()
    {
        GameObject newObject = Instantiate(this.prefabToSpawn);
        newObject.transform.position = new Vector2(Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
        newObject.transform.SetParent (transform);

       
        //objectSprites = cutController.dish.recipe;

        //instantiate a random sprite
        Sprite objectSprite = dishSprites[Random.Range(0, this.dishSprites.Count)];
        newObject.GetComponent<SpriteRenderer>().sprite = objectSprite;
    }
}