using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.IO;
using TMPro;

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
    private TicketController ticketController;
    private GameObject currentTray;

    public List<GameObject> orderStands = new List<GameObject>(); 
    public GameObject orderNumberPrefab;
    public int teamNumber;
    private int orderNum;

    private static GlobalTimer timer = new GlobalTimer();
    //private static RepeatLock rLock = new RepeatLock();

    void Start()
    {
       
        TC = GetComponent<TrayController>();
        ticketController = GetComponent<TicketController>();

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

    // Sets justClicked to correct ticket given the tray name 
    public void TrayOrderOptions(string trayName)
    {
        orderMenu.SetActive(true);
        justClicked = ticketController.GetCorrectTicket(trayName);
    }

    // serve corresponding order depending on what justClicked equals
    public async void Serve(GameObject justClicked)
    {
        
        justClicked.GetComponent<PhotonView>().RPC("SetToF", RpcTarget.All,
            justClicked.GetComponent<PhotonView>().ViewID);
        
        orderMenu.SetActive(false);

        DisplayTicket d_ticket = justClicked.GetComponent<DisplayTicket>();

        //SOUND -------------------------------------------------
        //RPC to all
        //FindObjectOfType<SoundEffectsManager>().servingSound.Play();

        /*foreach (GameObject t in TC.trays)
        {
            Tray ts = t.GetComponent<Tray>();
            if (ts.tray.trayID == d_ticket.orderid)
            {
                GameObject currentTray = t;
                Debug.Log("trays in the loop: " + t.name);
                
            }
        }
        Debug.Log("tray " + currentTray.name);
        currentTray.GetComponent<PhotonView>().RPC("PlayServingSound", RpcTarget.All, currentTray);*/
        //-------------------------------------------------------


        TC.CompareOrder(d_ticket.orderid);
       
        
        d_ticket.GetComponent<PhotonView>().RPC("clearAll", RpcTarget.All);
        
        
    }

    public void ShowNewTicketWithID(string order)
    {
        int orderN;
        if (ticket1.activeSelf == true)
        {
            if (ticket2.activeSelf == true)
            {
                DisplayTicket Ticket3 = ticket3.GetComponent<DisplayTicket>();
                ticket3.SetActive(true);
                Ticket3.orderNumber = 3;

                orderN = DisplayOrderFromID(Ticket3, order);
                orderStands[2].GetComponentInChildren<TextMeshProUGUI>().text = orderN.ToString(); 

            }

            else
            {
                DisplayTicket Ticket2 = ticket2.GetComponent<DisplayTicket>();
                ticket2.SetActive(true);
                Ticket2.orderNumber = 2;
                orderN = DisplayOrderFromID(Ticket2, order);
                orderStands[1].GetComponentInChildren<TextMeshProUGUI>().text =orderN.ToString(); 

            }
        }

        else
        {
            DisplayTicket Ticket1 = ticket1.GetComponent<DisplayTicket>();
            ticket1.SetActive(true);
            Ticket1.orderNumber = 1;

            orderN = DisplayOrderFromID(Ticket1, order);
            orderStands[0].GetComponentInChildren<TextMeshProUGUI>().text =orderN.ToString(); 
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
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
                {
                    this.GetComponent<PhotonView>().RPC("ShowingWithOrderTeam1", RpcTarget.All, leaderOrder.orderID);
                } else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
            {
                this.GetComponent<PhotonView>().RPC("ShowingWithOrderTeam2", RpcTarget.All, leaderOrder.orderID);
            }
              
               
            }
            else if (CheckIfTicketsNotFull() && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
            {
                Order leaderOrder1 = GetNewRandomOrder();

            // ONLY DO THIS TO TEAM 2
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
            {
                this.GetComponent<PhotonView>().RPC("ShowingWithOrderTeam2", RpcTarget.All, leaderOrder1.orderID);
            }
                   
        }

    }

    public int DisplayNewRandomOrder(DisplayTicket ticket)
    {  
        // RANDOM ORDER
        Order o = Database.GetRandomOrder();
        string orderID = o.orderID;
       
        TrayController tray_Controller = gameObject.GetComponent<TrayController>();
        tray_Controller.makeTray(orderID);
        o.orderNumber  = ++orderNum;
        ticket.SetUI(o);
        return o.orderNumber;

    }

    public Order GetNewRandomOrder()
    {
        // RANDOM ORDER
        return Database.GetRandomOrder();
    }

    public int DisplayOrderFromID(DisplayTicket ticket, string order)
    {
        Order o = Database.GetOrderByID(order);
        string orderID = o.orderID;

        TC.makeTray(orderID);

        o.orderNumber = ++orderNum;
        ticket.SetUI(o);
        return o.orderNumber;

    }

    [PunRPC]
    void setSignParent(int viewIDSign, int viewIDTray){
        PhotonView.Find(viewIDSign).transform.SetParent(PhotonView.Find(viewIDTray).transform);
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

    //SOUND ---------------------------------------------------------------
    /*[PunRPC]
    void PlayServingSound() {
        FindObjectOfType<SoundEffectsManager>().servingSound.Play();
        //obj.GetComponent<AudioSource>().Play();
    }

    [PunRPC]
    void PlayServingSound(GameObject obj) {
        //FindObjectOfType<SoundEffectsManager>().servingSound.Play();
        obj.GetComponent<AudioSource>().Play();
    } */
    //----------------------------------------------------------------------


   /* [PunRPC]
    void ShowClientTicket(string name)
    {
        ServeClient(name);
    }*/

    

}
