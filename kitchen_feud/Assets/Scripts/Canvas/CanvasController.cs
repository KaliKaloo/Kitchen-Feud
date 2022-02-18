using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class CanvasController : MonoBehaviour
{
    //public GameObject makeTicket;
    public GameObject ticket1;
    public GameObject ticket2;
    public GameObject ticket3;
    public GameObject orderMenu;
    public Button serve;
    public GameObject justClicked;
    public TrayController TC;


    private int orderNum;

    private static GlobalTimer timer = new GlobalTimer();

    void Start()
    {
        TC = GetComponent<TrayController>();
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
        //makeTicket.SetActive(true);

        TrayController tray_Controller = gameObject.GetComponent<TrayController>();
        DisplayTicket d_ticket = justClicked.GetComponent<DisplayTicket>();

        tray_Controller.resetTray(d_ticket.orderid);
    }


    public void ShowNewTicket()
    {
        if ((ticket3.activeSelf == true) && (ticket1.activeSelf == true) && (ticket2.activeSelf == true))
        {
            //disable button
            //makeTicket.SetActive(false);
        }

        if (ticket1.activeSelf == true)
        {
           if (ticket2.activeSelf == true)
            {
                DisplayTicket Ticket3 = ticket3.GetComponent<DisplayTicket>();
                ticket3.SetActive(true);            
                DisplayNewRandomOrder(Ticket3);
                
            }
            
            else
            {
                DisplayTicket Ticket2 = ticket2.GetComponent<DisplayTicket>();
                ticket2.SetActive(true);
                DisplayNewRandomOrder(Ticket2);
            }
        }

        else
        {
            DisplayTicket Ticket1 = ticket1.GetComponent<DisplayTicket>();
            ticket1.SetActive(true);
            DisplayNewRandomOrder(Ticket1);
        }

       
    }

    private bool CheckIfTicketsNotFull()
    {
        if (ticket1.activeSelf && ticket2.activeSelf && ticket3.activeSelf)
        {
            //this.GetComponent<PhotonView>().RPC("showT", RpcTarget.Others, ticket1.GetComponent<PhotonView>().ViewID, ticket2.GetComponent<PhotonView>().ViewID, ticket3.GetComponent<PhotonView>().ViewID);
            return false;
        }
        else
        {
            return true;
        }

        
    }

    private void UpdateOrders()
    {
        // LEADER OF TEAM 1
        if (CheckIfTicketsNotFull())
        {
            this.GetComponent<PhotonView>().RPC("Showing", RpcTarget.All);
        }
        /*
        if (TC.teamNumber == 1)
        {
            if (CheckIfTicketsNotFull())
            {
                this.GetComponent<PhotonView>().RPC("Showing", RpcTarget.All);
                //ShowNewTicket();
            }
        } 
        // IF USER IS LEADER OF TEAM 2
        else 
        {
            if (CheckIfTicketsNotFull())
            {
                this.GetComponent<PhotonView>().RPC("Showing", RpcTarget.All);
                //ShowNewTicket();
            }
        }
        */
    }

    public void DisplayNewRandomOrder(DisplayTicket ticket)
    {  
        // RANDOM ORDER
        Order o = Database.GetRandomOrder();
        string orderID = o.orderID;
       
        TrayController tray_Controller = gameObject.GetComponent<TrayController>();
        tray_Controller.makeTray(orderID);
        o.orderNumber  = ++orderNum;
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
