using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//using outlineEffect;

//public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
public class Hover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //public Database database;
    public GameObject tooltip;
    private SpriteRenderer sprite;
    // private bool mouse_over = false;
    public TextMeshProUGUI dish;
    public DisplayTicket displayticket;
    //public Sprite img;
    public GameObject RecipeCard;
    public Texture2D cursorClickable;
    public GlowController GlowController;
   


    public void OnPointerClick(PointerEventData eventData)
    {
         
            
            if (!RecipeCard.activeInHierarchy){
                GlowController.firstClick = true;
                Debug.Log("this is a first click");
            }

            if (displayticket.dishes.ContainsKey(dish.text)){
                GlowController.newClick = true;
                RecipeCard.SetActive(true);
                RecipeCard.GetComponent<Image>().sprite = displayticket.dishes[dish.text];
                GlowController.GlowAll(dish.text);

            }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //mouse_over = true;
        Cursor.SetCursor(cursorClickable, Vector2.zero, CursorMode.ForceSoftware);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // mouse_over = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    // public void RecipeCardClosed(){
    //     RecipeCard.SetActive(false);
    //     GlowController.firstClick = true;
    // }
    
    
}
