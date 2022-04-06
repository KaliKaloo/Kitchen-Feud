using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequiredComponent(typeof(BoxCollider))]

public class Clickable : MonoBehaviour
{
   public void OnMouseEnter(){
       MouseControl.instance.Clickable();
    }
    
    public void OnMouseExit(){
       
        MouseControl.instance.Default();
    }
}
