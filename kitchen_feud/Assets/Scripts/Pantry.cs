using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantry : MonoBehaviour
{
    public SubroomObject container;

    public void OnTriggerEnter(Collider other){
        var pickable = other.GetComponent<pickableItem>();

        if(pickable){
            if(pickable.item.location == Location.Pantry){
                container.AddItem(pickable.item, 1);
                Destroy(other.gameObject);
                Debug.Log(pickable.item.name+" put back in pantry");
            }
        }
    }

    private void OnApplicationQuit() {
        container.Container.Clear();
    }
    
}
