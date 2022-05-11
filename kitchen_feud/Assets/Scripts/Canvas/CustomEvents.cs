using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomEvents : EventTrigger
{
    public override void OnPointerEnter(PointerEventData eventData){
        if (gameObject.GetComponent<Hover>()){
                if (gameObject.GetComponent<Hover>().dish.text != ""){
                        MouseControl.instance.Clickable();
                    }
                else if(gameObject.GetComponent<Hover>().dish.text == ""){
                    MouseControl.instance.Default();
                }
        }
        
        else{
            MouseControl.instance.Clickable();
        }
    }
    
    public override void OnPointerExit(PointerEventData eventData){
       
        MouseControl.instance.Default();
    }
}
