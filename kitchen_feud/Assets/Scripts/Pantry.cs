using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantry : MonoBehaviour
{
    public PantryObject container;

    public void OnTriggerEnter(Collider other){
        var item = other.GetComponent<pickableItem>();

        if(item && item.item.Type == ItemType.Ingredient){
            container.AddItem(item.item, 1);
            Destroy(other.gameObject);
            Debug.Log(item.item.name+" put back in pantry");
        }
    }

    private void OnApplicationQuit() {
        container.Container.Clear();
    }
    
}
