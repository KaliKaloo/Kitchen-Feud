using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Interactable
{
   // public BaseFood item; 
    private ParticleSystem PS; 
    private bool click = true;
    public override void Interact()
    { 
        if (click == false){
            PS.Stop();
            click = true;
        }
        else {
            click = false;
            PS = gameObject.GetComponentInChildren<ParticleSystem>();
            PS.Play();
        }
    }
}
