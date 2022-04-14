using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Texture2D cursorClickable, cursorDefault;
    public static MouseControl instance;
   
    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else{
            Destroy(gameObject);
        }
    }

    public void Clickable(){
        Cursor.SetCursor(cursorClickable, Vector2.zero, CursorMode.Auto);
    }
    
    public void Default(){
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    
}

