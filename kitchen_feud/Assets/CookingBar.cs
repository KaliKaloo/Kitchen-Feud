using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingBar : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void SetCookedLevel(int value)
    {
        slider.value = value;
    }
}