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
    private bool mouse_over = false;
    public TextMeshProUGUI dish;
    public DisplayTicket displayticket;
    public Sprite img;
    public GameObject RecipeCard;
    public Texture2D cursorClickable;


    // void Update()
    // {
    //     if (mouse_over)
    //     {
 
            
    //         Debug.Log("over on" + dish.text);
   
    //     }
    // }

    

    public void OnPointerClick(PointerEventData eventData)
    {
        bool click = true;
        // Debug.Log("Mouse enter");
          RecipeCard.SetActive(true);
            
            // Debug.Log("clicked on" + dish.text);
            if (displayticket.dishes.ContainsKey(dish.text))
            {
                //hoverImage.sprite = displayticket.dishes[dish.text];
                RecipeCard.GetComponent<Image>().sprite = displayticket.dishes[dish.text];
                GlowAppliance(dish.text);
            }
            //tooltip.SetActive(true);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Cursor.SetCursor(cursorClickable, Vector2.zero, CursorMode.ForceSoftware);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    public GameObject[] appliancesToGlow;
    public GameObject glowAppliance;
    
    public void GlowAppliance(string dishName){
        DishSO dishInFocus = Database.GetDishByName(dishName);
        string dishTag = dishInFocus.toCook;
        Debug.Log(dishTag);

        appliancesToGlow = GameObject.FindGameObjectsWithTag(dishTag);

        // foreach (GameObject glowAppliance in appliancesToGlow)
        // {
        //     //turn on the glow script
        //    glowAppliance.GetComponent<OutlineEffect>().enabled = true;
           
        // }


    }
     

}

