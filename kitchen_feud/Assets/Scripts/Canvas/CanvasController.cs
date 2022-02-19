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

    public int teamNumber;
    private int orderNum;

    private static GlobalTimer timer = new GlobalTimer();
    //private static RepeatLock rLock = new RepeatLock();

    void Start()
    {
       
            TC = GetComponent<TrayController>();
            ticket1.SetActive(false);
            ticket2.SetActive(false);
            ticket3.SetActive(false);
            orderMenu.SetActive(false);
       

        // 1st parameter is how long till 1st order added
        // 2nd parameter is how many seconds till another order is added
        InvokeRepeating("UpdateOrders", 5, 5);

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
        justClicked.GetComponent<PhotonView>().RPC("SetToF", RpcTarget.All,
            justClicked.GetComponent<PhotonView>().ViewID);
        
        //justClicked.SetActive(false);
        orderMenu.SetActive(false);
        //makeTicket.SetActive(true);

        //TrayController tray_Controller = gameObject.GetComponent<TrayController>();
        DisplayTicket d_ticket = justClicked.GetComponent<DisplayTicket>();
        

        //TC.resetTray(d_ticket.orderid);
       
        TC.GetComponent<PhotonView>().RPC("resetAcross", RpcTarget.All, d_ticket.orderid);
        d_ticket.GetComponent<PhotonView>().RPC("clearAll", RpcTarget.All);
        
    }

    public void ServeClient(string name)
    {
        orderMenu.SetActive(false);
        //makeTicket.SetActive(true);

        TrayController tray_Controller = gameObject.GetComponent<TrayController>();
        DisplayTicket d_ticket = GameObject.Find(name).GetComponent<DisplayTicket>();
        GameObject.Find(name).SetActive(false);

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
                Ticket3.orderNumber = 3;
                DisplayNewRandomOrder(Ticket3);
                
            }
            
            else
            {
                DisplayTicket Ticket2 = ticket2.GetComponent<DisplayTicket>();
                ticket2.SetActive(true);
                Ticket2.orderNumber = 2;
                DisplayNewRandomOrder(Ticket2);
            }
        }

        else
        {
            DisplayTicket Ticket1 = ticket1.GetComponent<DisplayTicket>();
            ticket1.SetActive(true);
            Ticket1.orderNumber = 1;
            DisplayNewRandomOrder(Ticket1);
        }
    }

    public void ShowNewTicketWithID(string order)
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
                Ticket3.orderNumber = 3;

                DisplayOrderFromID(Ticket3, order);

            }

            else
            {
                DisplayTicket Ticket2 = ticket2.GetComponent<DisplayTicket>();
                ticket2.SetActive(true);
                Ticket2.orderNumber = 2;

                DisplayOrderFromID(Ticket2, order);
            }
        }

        else
        {
            DisplayTicket Ticket1 = ticket1.GetComponent<DisplayTicket>();
            ticket1.SetActive(true);
            Ticket1.orderNumber = 1;

            DisplayOrderFromID(Ticket1, order);
        }
    }

    private bool CheckIfTicketsNotFull()
    {
        if (ticket1.activeSelf && ticket2.activeSelf && ticket3.activeSelf)
        {
            
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
            if (CheckIfTicketsNotFull() && PhotonNetwork.IsMasterClient)
            {
                // OTHERS PART OF TEAM 1
                Order leaderOrder = GetNewRandomOrder();

                // ONLY DO THIS TO TEAM 1
                if (teamNumber == 1)
                {
                    this.GetComponent<PhotonView>().RPC("ShowingWithOrderTeam1", RpcTarget.All, leaderOrder.orderID);
                }
              
               
            }
            else if (CheckIfTicketsNotFull() && PhotonNetwork.PlayerList.GetValue(1).Equals(PhotonNetwork.LocalPlayer))
            {
                Order leaderOrder1 = GetNewRandomOrder();

            // ONLY DO THIS TO TEAM 2
            if (teamNumber == 2)
            {
               
                this.GetComponent<PhotonView>().RPC("ShowingWithOrderTeam2", RpcTarget.All, leaderOrder1.orderID);
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
        o.orderNumber  = ++orderNum;
        ticket.SetUI(o);

    }

    public Order GetNewRandomOrder()
    {
        // RANDOM ORDER
        return Database.GetRandomOrder();
    }

    public void DisplayOrderFromID(DisplayTicket ticket, string order)
    {
        Order o = Database.GetOrderByID(order);
        string orderID = o.orderID;

        TC.makeTray(orderID);

        o.orderNumber = ++orderNum;
        ticket.SetUI(o);

    }


    [PunRPC]
    void ShowingWithOrderTeam1(string o)
    {
        
            ShowNewTicketWithID(o);
        
    }

    [PunRPC]
    void ShowingWithOrderTeam2(string o)
    {
      
            ShowNewTicketWithID(o);
        
    }


    [PunRPC]
    void ShowClientTicket(string name)
    {
        ServeClient(name);
    }

    

}
