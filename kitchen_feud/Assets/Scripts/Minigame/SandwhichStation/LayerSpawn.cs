using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    private float gridX = 1;
    private float gridY = 4;
    private float spacing = Screen.height/6;

    public void StartSpawn(List<string> idList)
    {
            for (int y = 1; y <= gridY; y++)
            {
                for (int x = 0; x < gridX; x++)
                {
                    Vector3 pos = new Vector3(Screen.width/2, y * spacing, 0) ;
                    GameObject newObject = Instantiate(prefab, pos, Quaternion.identity);
                    newObject.transform.SetParent(transform);

                    newObject.GetComponent<SandwichMove>().LayerID = idList[y-1];
                    newObject.GetComponent<SandwichMove>().speed = Random.Range(550, 730);
                    //newObject.GetComponent<Image>().sprite = dictionary by id
                }
            }
        
    }
  
}
