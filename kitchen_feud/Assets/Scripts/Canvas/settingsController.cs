using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class settingsController : MonoBehaviour
{
    GameObject settingsButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            exitSettings();
        }
    }

    public void showSettings(){
        settingsButton = GameObject.Find("Settings Button");
        if (settingsButton)
            settingsButton.SetActive(false);
        gameObject.SetActive(true);
    }

    public void exitSettings(){
        gameObject.SetActive(false);
        if (settingsButton)
            settingsButton.SetActive(true);
    }

}
