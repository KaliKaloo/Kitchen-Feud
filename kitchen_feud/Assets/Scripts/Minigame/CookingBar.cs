using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingBar : MonoBehaviour
{
    public Slider slider;
    public float step;
    private bool keyHeld;
    private bool done;

    //public DishSO foundDish;

    public float abs(float x) {
       float result = x < 0 ? -x : x;
       return result;
    }

    void Start () {
        slider.value = -30;
        keyHeld = false;
        done = false;
    }

    //how do we setCookedLevel?
    //isFoundDish carried across scenes?
    void Update() {
        if (Input.GetKey(KeyCode.X) && done == false) {
            slider.value = slider.value + step;
            keyHeld = true;
        }
        else if(keyHeld == true && !Input.GetKey(KeyCode.X) && done == false) {
            //SetCookedLevel(slider.value);
            Debug.Log(100 - abs(slider.value));
            done = true;
            
        }
    }
    public void SetCookedLevel(int value)
    {
        //foundDish.finalScore = 100 - abs(slider.value);
    }
}