using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SandwichSpawner : MonoBehaviour
{
    public SandwichController SandwichController;
    public GameObject prefabToSpawn;


    public GameObject spawnObject(IngredientSO i){
        GameObject newObject = Instantiate(this.prefabToSpawn);
        newObject.transform.SetParent(transform);
        
        GameObject myParentObject = gameObject;
        newObject.transform.SetParent(myParentObject.transform, false);
        newObject.transform.position = myParentObject.transform.position;
        newObject.GetComponent<Image>().sprite = i.img;
        newObject.GetComponent<SandwichID>().Id = i.ingredientID;
         
        return newObject;
    }

    public void DestroySandwich(){
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
