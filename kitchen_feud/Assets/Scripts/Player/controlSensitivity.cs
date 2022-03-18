using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlSensitivity : MonoBehaviour
{
 
    int n;
    public Text myText;
    public Slider mySlider;
    void Update() {
        myText.text = "Current Volume: " + mySlider.value;
    }
}
