using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    // Start is called before the first frame update
    public int Id;
    public GameObject prefab;
    public float gridX = 1;
    public float gridY = 4;
    public float spacing = 1000;

    public void StartSpawn()
    {

        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, 0, y) * spacing;
                GameObject newObject = Instantiate(prefab, pos, Quaternion.identity);
                newObject.transform.SetParent(transform);
            }
        }
    }

  
}
