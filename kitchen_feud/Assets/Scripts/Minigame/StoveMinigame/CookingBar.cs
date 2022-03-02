using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookingBar : MonoBehaviour
{
    public Slider slider;
    public float step;
    public bool keyHeld;
    public bool done;
    public float cookedLevel;
 
    void Start () {
        slider.value = -30;
        keyHeld = false;
        done = false;

    }

    void Update() {
        if (Input.GetKey(KeyCode.X) && done == false) {
            slider.value = slider.value + step;
            keyHeld = true;
        }
        else if(keyHeld == true && !Input.GetKey(KeyCode.X) && done == false) {
            //EVENT SYSTEM: EMIT AN EVENT WHEN SET VALUE
            cookedLevel = SetCookedLevel(slider.value);
            done = true;
            
            GameEvents.current.assignPointsEventFunction();
        }
    }

    public float SetCookedLevel(float value)
    {
        return 100f - abs(value);
    }

    public float abs(float x) {
       return x < 0 ? -x : x;
    }
}