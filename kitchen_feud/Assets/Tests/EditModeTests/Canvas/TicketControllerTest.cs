using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TicketControllerTest
{

    TicketController ticketController;

    [OneTimeSetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        ticketController = obj.AddComponent<TicketController>();
        ticketController.ticket1x1 =  new GameObject();
        ticketController.ticket2x1 =  new GameObject();
        ticketController.ticket3x1 =  new GameObject();
        ticketController.ticket1x2 =  new GameObject();
        ticketController.ticket2x2 =  new GameObject();
        ticketController.ticket3x2 =  new GameObject();

    }

    [Test]
    public void CorrectTicketk1Tray1()
    {
        GameObject ticket = ticketController.GetCorrectTicket("Tray1-1");
        Assert.AreEqual(ticketController.ticket1x1, ticket, "tray returns wrong ticket");
    }

    [Test]
    public void CorrectTicketk1Tray2()
    {
        GameObject ticket = ticketController.GetCorrectTicket("Tray2-1");
        Assert.AreEqual(ticketController.ticket2x1, ticket, "tray returns wrong ticket");
    }

    [Test]
    public void CorrectTicketk1Tray3()
    {
        GameObject ticket = ticketController.GetCorrectTicket("Tray3-1");
        Assert.AreEqual(ticketController.ticket3x1, ticket, "tray returns wrong ticket");
    }


    [Test]
    public void CorrectTicketk2Tray1()
    {
        GameObject ticket = ticketController.GetCorrectTicket("Tray1-2");
        Assert.AreEqual(ticketController.ticket1x2, ticket, "tray returns wrong ticket");
    }

    [Test]
    public void CorrectTicketk2Tray2()
    {
        GameObject ticket = ticketController.GetCorrectTicket("Tray2-2");
        Assert.AreEqual(ticketController.ticket2x2, ticket, "tray returns wrong ticket");
    }

    [Test]
    public void CorrectTicketk2Tray3()
    {
        GameObject ticket = ticketController.GetCorrectTicket("Tray3-2");
        Assert.AreEqual(ticketController.ticket3x2, ticket, "tray returns wrong ticket");

    }

   
}
