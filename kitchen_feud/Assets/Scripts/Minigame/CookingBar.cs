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
    public Stove stove;

    void Start () {
        slider.value = -30;
        keyHeld = false;
        done = false;

        //slider.onValueChanged.AddListener(delegate { UpdateDishPoints();});

    }

    //GET RID OF THE UPDATE FUNCTION
    void Update() {
        if (Input.GetKey(KeyCode.X) && done == false) {
            slider.value = slider.value + step;
            keyHeld = true;
        }
        else if(keyHeld == true && !Input.GetKey(KeyCode.X) && done == false) {
            //CUSTOM EVENT SYSTEM: EMIT AN EVENT WHEN SET VALUE
            cookedLevel = SetCookedLevel(slider.value);
            done = true;
            GameEvents.current.assignPointsEventFunction();
            
        }
    }
/*
    //sets back to 70 too! I think it counts the reset as a value change
    public void UpdateDishPoints() {
        cookedLevel = SetCookedLevel(slider.value);
        stove.dishOfFoundDish.points = cookedLevel;
        Debug.Log(stove.dishOfFoundDish.points);
    }*/

    public float SetCookedLevel(float value)
    {
        return 100f - abs(value);
    }

    public float abs(float x) {
       float result = x < 0 ? -x : x;
       return result;
    }
}