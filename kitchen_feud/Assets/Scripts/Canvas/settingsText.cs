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
            setting/=10;
        }else if (transform.parent.parent.name == "Music Volume"){
            setting*=10;
        }else {
            setting *= 0.625f;
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = Math.Round(setting, 1).ToString();
    }
}
