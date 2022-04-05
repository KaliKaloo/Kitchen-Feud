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
    private SpriteRenderer sprite;
    public TextMeshProUGUI dish;
    public DisplayTicket displayticket;
    public GameObject RecipeCard;
    public Texture2D cursorClickable;
    public GlowController GlowController;
   


    public void OnPointerClick(PointerEventData eventData)
    {

            if (!RecipeCard.activeInHierarchy){
                GlowController.firstClick = true;
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
        if (dish.text != ""){
            Cursor.SetCursor(cursorClickable, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
    

    
}
