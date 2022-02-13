using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingBar : MonoBehaviour
{
    public Slider slider;
    public float step;

    void Start () {
        slider.value = 0;
    }
    void Update() {
        if (Input.GetKey(KeyCode.X)) {
            slider.value = slider.value + step;
        }
    }
    public void SetCookedLevel(int value)
    {
        slider.value = value;
    }
}