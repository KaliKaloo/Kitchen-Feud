using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    private SpriteRenderer sprite;
    private bool mouse_over = false;
    public TextMeshProUGUI dish;
    public DisplayTicket displayticket;
    public Sprite img;

    void Update()
    {
        if (mouse_over)
        {
            Image hoverImage = tooltip.GetComponent<Image>();
            hoverImage.sprite = displayticket.dishes[dish.text];
            tooltip.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        // Debug.Log("Mouse enter");
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        // Debug.Log("Mouse exit");
        tooltip.SetActive(false);
    }

}

