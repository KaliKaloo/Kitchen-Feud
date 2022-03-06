using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    //public delegate ObjectSpawnedHandler (CuttableObject obj);
    //public event ObjectSpawnedHandler OnObjectSpawned;
    
    public GameObject prefabToSpawn;

    public float spawnInterval;
    public float objectMinX;
    public float objectMaxX;
    public float objectY;
    private Image h;

    public List<Sprite> displaySprites = new List<Sprite>();

    public cutController CutController;

    public void StartSpawn(List<Sprite> dishSprites)
    {
        InvokeRepeating("spawnObject", spawnInterval, spawnInterval);
        displaySprites = dishSprites;
    }

    private void spawnObject()
    {
        GameObject newObject = Instantiate(this.prefabToSpawn);
        newObject.transform.position = new Vector2(Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
        newObject.transform.SetParent(transform);


        //instantiate a random sprite
        h = newObject.GetComponent<Image>();
        Sprite objectSprite = displaySprites[Random.Range(0, displaySprites.Count)];
        h.sprite = objectSprite;
    }
}