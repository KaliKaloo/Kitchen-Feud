using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseControl : MonoBehaviour
{
    public Texture2D cursorClickable;
    public static MouseControl instance;
    private bool set = false;
   
    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else{
            Destroy(gameObject);
        }
    }
    private void Update()
    {
       if(SceneManager.GetActiveScene().name == "gameOver" && !set)
        {
            Cursor.visible = true;
            set = true;
        }
    }
    public void Clickable(){
        Cursor.SetCursor(cursorClickable, Vector2.zero, CursorMode.Auto);
    }
    
    public void Default(){
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    
}

