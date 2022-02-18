using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class CanvasController : MonoBehaviour
{
    public GameObject makeTicket;
    public GameObject ticket1;
    public GameObject ticket2;
    public GameObject ticket3;
    public GameObject orderMenu;
    public Button serve;
    public GameObject justClicked;

    private static GlobalTimer timer = new GlobalTimer();

    void Start()
    {
        ticket1.SetActive(false);
        ticket2.SetActive(false);
        ticket3.SetActive(false);
        orderMenu.SetActive(false);

        // 1st parameter is how long till 1st order added
        // 2nd parameter is how many seconds till another order is added
        InvokeRepeating("UpdateOrders", 0, 5);

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

        TrayController tray_Controller = gameObject.GetComponent<TrayController>();
        DisplayTicket d_ticket = justClicked.GetComponent<DisplayTicket>();

        tray_Controller.resetTray(d_ticket.orderid);
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
                DisplayTicket Ticket3 = ticket3.GetComponent<DisplayTicket>();
                Debug.LogError("Well you're here");
                ticket3.SetActive(true);            
                DisplayNewRandomOrder(Ticket3);
                
            }
            
            else
            {
                DisplayTicket Ticket2 = ticket2.GetComponent<DisplayTicket>();
                Debug.LogError("Well you're here");
                ticket2.SetActive(true);
                
                
                DisplayNewRandomOrder(Ticket2);
            }
        }

        else
        {
            DisplayTicket Ticket1 = ticket1.GetComponent<DisplayTicket>();
            Debug.LogError("Well you're here");
            ticket1.SetActive(true);
            DisplayNewRandomOrder(Ticket1);
        }

       
    }

    private bool CheckIfTicketsFull()
    {
        if (ticket1.activeInHierarchy && ticket2.activeInHierarchy && ticket3.activeInHierarchy)
        {
            this.GetComponent<PhotonView>().RPC("showT", RpcTarget.Others, ticket1.GetComponent<PhotonView>().ViewID, ticket2.GetComponent<PhotonView>().ViewID, ticket3.GetComponent<PhotonView>().ViewID);
           
            return false;
        } else
        {
            return true;
        }
    }

    private void UpdateOrders()
    {
        // LEADER OF TEAM 1
        if (PhotonNetwork.IsMasterClient)
        {
            if (CheckIfTicketsFull())
            {
                this.GetComponent<PhotonView>().RPC("Showing", RpcTarget.Others);
                ShowNewTicket();
            }
        } 
        // IF USER IS LEADER OF TEAM 2
        else if (false)
        {
            if (CheckIfTicketsFull())
            {
                this.GetComponent<PhotonView>().RPC("Showing", RpcTarget.Others);
                ShowNewTicket();
            }
        }
    }

    public void DisplayNewRandomOrder(DisplayTicket ticket)
    {  
        // RANDOM ORDER
        Order o = Database.GetRandomOrder();
        string orderID = o.orderID;
       
        TrayController tray_Controller = gameObject.GetComponent<TrayController>();
        tray_Controller.makeTray(orderID);
        ticket.SetUI(o);

    }

    [PunRPC]
    void showT(int x,int y, int z){
        PhotonView.Find(x).gameObject.SetActive(true);
        //PhotonView.Find(x).GetComponent<DisplayTicket>().SetUI(PhotonView.Find(x).GetComponent<DisplayTicket>().order1);
        PhotonView.Find(y).gameObject.SetActive(true);
        PhotonView.Find(z).gameObject.SetActive(true);
        //DisplayNewRandomOrder(PhotonView.Find(y).GetComponent<DisplayTicket>());

    }
    [PunRPC]
    void Showing()
    {
        ShowNewTicket();
    }
   
}
