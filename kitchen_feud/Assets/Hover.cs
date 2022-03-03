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
    //public TextMesh text;
    public TextMeshProUGUI text;
    private SpriteRenderer sprite;
    
    //public RectTransform background;
    private bool mouse_over = false;

   /* private void OnMouseOver()
    {
        text.text = gameObject.GetComponent<DisplayTicket>().orderMainText.text;
        tooltip.SetActive(true);
        Debug.LogError("HELLO");
    }
    private void OnMouseExit() 
    {
        tooltip.SetActive(false);
    }*/
    void Update()
    {
        if (mouse_over)
        {
            Debug.LogError("Mouse Over");

            text.text = "<sprite=0>" + " + " + "<sprite=0>";
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

