using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

//public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
public class Hover : MonoBehaviour, IPointerClickHandler
{
    public GameObject tooltip;
    private SpriteRenderer sprite;
    private bool mouse_over = false;
    public TextMeshProUGUI dish;
    public DisplayTicket displayticket;
    public Sprite img;
    public GameObject RecipeCard;


    // void Update()
    // {
    //     if (mouse_over)
    //     {
    //         //Image hoverImage = tooltip.GetComponent<Image>();
    //         RecipeCard.SetActive(true);
            
    //         Debug.Log("clicked on" + dish.text)
    //         if (displayticket.dishes.ContainsKey(dish.text))
    //         {
    //             //hoverImage.sprite = displayticket.dishes[dish.text];
    //             RecipeCard.GetComponent<Image>().sprite = displayticket.dishes[dish.text];
    //         }
    //         //tooltip.SetActive(true);
    //     }
    // }

    

    public void OnPointerClick(PointerEventData eventData)
    {
        mouse_over = true;
        // Debug.Log("Mouse enter");
          RecipeCard.SetActive(true);
            
            Debug.Log("clicked on" + dish.text);
            if (displayticket.dishes.ContainsKey(dish.text))
            {
                //hoverImage.sprite = displayticket.dishes[dish.text];
                RecipeCard.GetComponent<Image>().sprite = displayticket.dishes[dish.text];
            }
            //tooltip.SetActive(true);
        
    }

    // public void OnPointerClickExit(PointerEventData eventData)
    // {
    //     mouse_over = false;
    //     // Debug.Log("Mouse exit");
    //     tooltip.SetActive(false);
    // }

}

