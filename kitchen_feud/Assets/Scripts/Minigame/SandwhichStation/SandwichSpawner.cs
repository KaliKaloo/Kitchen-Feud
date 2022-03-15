using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandwichSpawner : MonoBehaviour
{
    public SandwichController SandwichController;
    public GameObject prefabToSpawn;
    //public IngredientSO iSO;
    public Image img;


    //SpawnLayers();
    public GameObject spawnObject(IngredientSO i){
        GameObject newObject = Instantiate(this.prefabToSpawn);
        newObject.transform.position = new Vector3(0,0,0);
        newObject.transform.SetParent(transform);
        //iSO = i;
        newObject.GetComponent<Image>().sprite = i.img;
        newObject.GetComponent<SandwichID>().Id = i.ingredientID;
         
        return newObject;
    }
}
