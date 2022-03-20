using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class settingsController : MonoBehaviour
{
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            exitSettings();
        }
    }

    public void showSettings(){
        gameObject.SetActive(true);
    }

    public void exitSettings(){
        gameObject.SetActive(false);
    }

}
