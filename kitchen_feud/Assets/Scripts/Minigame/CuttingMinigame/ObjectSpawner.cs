using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    
    public GameObject prefabToSpawn;

    public float spawnInterval;
    private float objectMinX;
    private float objectMaxX;
    public float objectY;
    private Image h;

    private List<Sprite> displaySprites = new List<Sprite>();

    private cutController CutController;

    public void StopSpawn(){
        CancelInvoke();
    }

    public void StartSpawn(List<Sprite> dishSprites)
    {
        objectMinX = Screen.width/5;
        objectMaxX = Screen.width - objectMinX;
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