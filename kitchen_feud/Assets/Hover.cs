using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//public class Hover : MonoBehaviour
{
    public GameObject tooltip;
    public TextMeshProUGUI text;
    private SpriteRenderer sprite;
    private bool mouse_over = false;
    public TextMeshProUGUI dish;
    IDictionary<string, int> dishes = new Dictionary<string, int>();

    void Start() {
        dishes.Add("Burger", 1);
        dishes.Add("Chips", 2);
        dishes.Add("Egg-fried Rice", 3);
        dishes.Add("Eggy Bread", 4);
        dishes.Add("Mushroom Soup", 5);
        dishes.Add("Omelette", 6);
        dishes.Add("Salad", 7);
        dishes.Add("Sandwich", 8);
        dishes.Add("Tomato Soup", 9);


    }


    void Update()
    {
        if (mouse_over)
        {
            Debug.LogError(dishes[dish.text]);

            //text.text = "<sprite=" +0+ ">" + " + " + "<sprite=" + 1 + ">"+ " = " + "<sprite=" + 2 + ">";
            text.text = "<sprite=" + dishes[dish.text] + ">";
            //text.text = "How does this work? Can you pleasee explain this to me?!?!?";
            //float textPadding = 4f;
            //Vector2 backgroundSize = new Vector2(text.preferredWidth + textPadding*2f,text.preferredHeight + textPadding * 2f);
            //Vector2 textSize = new Vector2(text.preferredWidth, text.preferredHeight);
            //tooltip.GetComponent<RectTransform>().sizeDelta = backgroundSize;
            //text.GetComponent<RectTransform>().sizeDelta =textSize;
            
            tooltip.SetActive(true);
            //tooltip.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("Mouse enter");
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Debug.Log("Mouse exit");
        tooltip.SetActive(false);
    }

}

