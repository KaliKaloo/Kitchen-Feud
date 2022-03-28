using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketController : MonoBehaviour
{
    // assign tickets in inspector
    // team 1 tickets
    public GameObject ticket1x1;
    public GameObject ticket2x1;
    public GameObject ticket3x1;

    // team 2 tickets
    public GameObject ticket1x2;
    public GameObject ticket2x2;
    public GameObject ticket3x2;

    // returns the corresponding ticket given the tray name
    public GameObject GetCorrectTicket(string trayName)
    {
        if (trayName == "Tray1-1")
            return ticket1x1;
        else if (trayName == "Tray2-1")
            return ticket2x1;
        else if (trayName == "Tray3-1")
            return ticket3x1;
        else if (trayName == "Tray1-2")
            return ticket1x2;
        else if (trayName == "Tray2-2")
            return ticket2x2;
        else 
            return ticket3x2;
    }
}
