using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.IO;
using System.Net.Http.Headers;
using ExitGames.Client.Photon.StructWrapping;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public GameObject ticket1;
    public GameObject ticket2;
    public GameObject ticket3;
    public GameObject orderMenu;
    public Button serve;
    public PhotonView PV;
    public GameObject justClicked;
    public TrayController TC;
    private TicketController ticketController;

    public List<GameObject> orderStands = new List<GameObject>();
    public GameObject orderNumberPrefab;
    public int teamNumber;
    private int orderNum;

    private static GlobalTimer timer = new GlobalTimer();

    void Start()
    {
        PV = GetComponent<PhotonView>();
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

    private void Update()
    {
        if (GameObject.Find("Local"))
        {

            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1 && GameObject.FindGameObjectWithTag("Team2") && GameObject.FindGameObjectWithTag("Team2").activeSelf)
            {
                GameObject[] Team2 = GameObject.FindGameObjectsWithTag("Team2");
                foreach (GameObject obj in Team2)
                {
                    obj.SetActive(false);
                }
            }
            else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2 && GameObject.FindGameObjectWithTag("Team1") && GameObject.FindGameObjectWithTag("Team1").activeSelf)
            {
                GameObject[] Team1 = GameObject.FindGameObjectsWithTag("Team1");
                foreach (GameObject obj in Team1)
                {
                    obj.SetActive(false);
                }
            }
        }

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
        justClicked = ticketController.GetCorrectTicket(trayName);
        if (justClicked.activeSelf)
        {
            orderMenu.SetActive(true);
        }
    }

    // serve corresponding order depending on what justClicked equals
    public void Serve(GameObject justClicked)
    {
        orderMenu.SetActive(false);

        DisplayTicket d_ticket = justClicked.GetComponent<DisplayTicket>();

        TC.CompareOrder(d_ticket.orderid, justClicked.GetPhotonView().ViewID);


        d_ticket.GetComponent<PhotonView>().RPC("clearAll", RpcTarget.All);


    }

    public void ShowNewTicketWithID(Order LeaderOrder) // already inside an rpc
    {
        if (PhotonNetwork.IsMasterClient)
        {
            string order = LeaderOrder.orderID;
            if (ticket1.activeSelf == true)
            {
                if (ticket2.activeSelf == true)
                {

                    PV.RPC("showTickets", RpcTarget.AllBuffered, PV.ViewID, ticket3.GetPhotonView().ViewID, 3, order);

                }

                else
                {
                    PV.RPC("showTickets", RpcTarget.AllBuffered, PV.ViewID, ticket2.GetPhotonView().ViewID, 2, order);

                }
            }

            else
            {
                PV.RPC("showTickets", RpcTarget.AllBuffered, PV.ViewID, ticket1.GetPhotonView().ViewID, 1, order);
            }
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

    // called every 5 seconds generating new tickets for each team
    private void UpdateOrders()
    {
        if (CheckIfTicketsNotFull() && PhotonNetwork.IsMasterClient)
        {
            Order leaderOrder = GetNewRandomOrder();
            ShowNewTicketWithID(leaderOrder);
        }

    }

    public int DisplayNewRandomOrder(DisplayTicket ticket)
    {
        // RANDOM ORDER
        Order o = Database.GetRandomOrder();
        string orderID = o.orderID;

        TrayController tray_Controller = gameObject.GetComponent<TrayController>();
        tray_Controller.makeTray(orderID);
        o.orderNumber = ++orderNum;
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
        ticket.SetUI(o);
        return o.orderNumber;

    }

    [PunRPC]
    void setSignParent(int viewIDSign, int viewIDTray)
    {
        PhotonView.Find(viewIDSign).transform.SetParent(PhotonView.Find(viewIDTray).transform);
    }

    [PunRPC]
    void showTickets(int viewID, int ticketID, int num, string order)
    {
        CanvasController CC = PhotonView.Find(viewID).GetComponent<CanvasController>();

        GameObject ticket = PhotonView.Find(ticketID).gameObject;
        DisplayTicket dTicket = ticket.GetComponent<DisplayTicket>();
        ticket.SetActive(true);
        dTicket.orderNumber = num;

        int orderN = DisplayOrderFromID(dTicket, order);
        CC.orderStands[num - 1].GetComponentInChildren<TextMeshProUGUI>().text = orderN.ToString();
    }

}
