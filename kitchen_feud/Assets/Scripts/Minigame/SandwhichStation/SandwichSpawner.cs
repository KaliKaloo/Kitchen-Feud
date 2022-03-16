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
        
       // new Vector3()= newObject.transform.SetParent(transform).position;
        newObject.transform.SetParent(transform);
        
        GameObject myParentObject = gameObject;
        newObject.transform.parent = myParentObject.transform;
        newObject.transform.position = myParentObject.transform.position;
       // newObject.transform.position = parent.transform.localPosition;
        
        newObject.GetComponent<Image>().sprite = i.img;
        newObject.GetComponent<SandwichID>().Id = i.ingredientID;
         
        return newObject;
    }
}
