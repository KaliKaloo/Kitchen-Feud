using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class settingsText : MonoBehaviour
{

    public void updateText(float setting){
        if (transform.parent.parent.name == "Rotation"){
            setting/=100;
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = Math.Round(setting, 1).ToString();
    }
}