using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasController : MonoBehaviour
{
    public GameObject makeTicket;
    public GameObject ticket1;
    public GameObject ticket2;
    public GameObject ticket3;
    public GameObject orderMenu;
    public Button serve;
    public GameObject justClicked;

    void Start()
    {
        ticket1.SetActive(false);
        ticket2.SetActive(false);
        ticket3.SetActive(false);
        orderMenu.SetActive(false);

        Button btn = serve.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        
    }

    public void TaskOnClick()
    {
        Serve(justClicked);
    }

    public void OrderOptions()
    {
        orderMenu.SetActive(true);
        justClicked = EventSystem.current.currentSelectedGameObject;
    }
    
    public void Serve(GameObject justClicked)
    {
        justClicked.SetActive(false);
        orderMenu.SetActive(false);
        makeTicket.SetActive(true);
    }


    public void ShowNewTicket()
    {
        if ((ticket3.activeInHierarchy == true) && (ticket1.activeInHierarchy == true) && (ticket2.activeInHierarchy == true))
        {
            //disable button
            makeTicket.SetActive(false);
        }

        if (ticket1.activeInHierarchy == true)
        {
           if (ticket2.activeInHierarchy == true)
            { 
                ticket3.SetActive(true);
                DisplayTicket Ticket3 = ticket3.GetComponent<DisplayTicket>();
                Ticket3.DisplayNewRandomOrder();
                
            }
            
            else
            { 
                ticket2.SetActive(true);
                DisplayTicket Ticket2 = ticket2.GetComponent<DisplayTicket>();
                Ticket2.DisplayNewRandomOrder();
            }
        }

        else
        {
            DisplayTicket Ticket1 = ticket1.GetComponent<DisplayTicket>();
            ticket1.SetActive(true);
            Ticket1.DisplayNewRandomOrder();
        }

       
    }
}
