using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    public TraySO tray;

    public void OnTriggerEnter(Collider other){
        Debug.Log("Collided!");

        var pickable = other.GetComponent<pickableItem>();

        if(pickable && tray.ServingTray.Count <3){
            tray.ServingTray.Add(pickable.item);
            Debug.Log(tray.ServingTray.Count);
        }
        
    }
}
 