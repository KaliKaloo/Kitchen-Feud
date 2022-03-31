using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;


public class DisplayTicketTest
{

    DisplayTicket displayTicket;
    Order order;
    DishSO dish1;
    DishSO dish2;
    DishSO dish3;

    
    [SetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        displayTicket = obj.AddComponent<DisplayTicket>();
        order = ScriptableObject.CreateInstance<Order>();
        dish1 = ScriptableObject.CreateInstance<DishSO>();
        dish2 = ScriptableObject.CreateInstance<DishSO>();
        dish3 = ScriptableObject.CreateInstance<DishSO>();

        dish1.name = "pizza";
        dish2.name = "chips";
        dish3.name = "water";
        order.orderID = "order ID";
        
        displayTicket.orderNumberText = obj.AddComponent<Text>();
        GameObject main = new GameObject();
        GameObject side = new GameObject();
        GameObject drink = new GameObject();
        displayTicket.orderMainText = main.AddComponent<TextMeshProUGUI>();
        displayTicket.orderSideText = side.AddComponent<TextMeshProUGUI>();
        displayTicket.orderDrinkText = drink.AddComponent<TextMeshProUGUI>();

    }


    [Test]
    public void checkOrderNumText()
    {
        order.orderNumber = 5;
        order.dishes.Add(dish1);
        displayTicket.SetUI(order);
        Assert.AreEqual("5", displayTicket.orderNumberText.text);
        Assert.AreEqual("order ID", displayTicket.orderid);

    }


    [Test]
    public void oneOrder()
    {
        order.dishes.Add(dish1);
        displayTicket.SetUI(order);
        Assert.AreEqual("pizza", displayTicket.orderMainText.text);
    }


    [Test]
    public void twoOrders()
    {
        order.dishes.Add(dish1);
        order.dishes.Add(dish2);
        displayTicket.SetUI(order);
        Assert.AreEqual("pizza", displayTicket.orderMainText.text);
        Assert.AreEqual("chips", displayTicket.orderSideText.text);

    }


    [Test]
    public void threeOrders()
    {
        order.dishes.Add(dish1);
        order.dishes.Add(dish2);
        order.dishes.Add(dish3);
        displayTicket.SetUI(order);
        Assert.AreEqual("pizza", displayTicket.orderMainText.text);
        Assert.AreEqual("chips", displayTicket.orderSideText.text);
        Assert.AreEqual("water", displayTicket.orderDrinkText.text);
        Assert.AreEqual(3, displayTicket.dishes.Count);
        Assert.IsTrue(displayTicket.dishes.ContainsKey("pizza"));
        Assert.IsTrue(displayTicket.dishes.ContainsKey("chips"));
        Assert.IsTrue(displayTicket.dishes.ContainsKey("water"));


    }


    [Test]
    public void repeatedOrders()
    {
        order.dishes.Add(dish1);
        order.dishes.Add(dish2);
        order.dishes.Add(dish2);
        displayTicket.SetUI(order);
        Assert.AreEqual("pizza", displayTicket.orderMainText.text);
        Assert.AreEqual("chips", displayTicket.orderSideText.text);
        Assert.AreEqual("chips", displayTicket.orderDrinkText.text);
        Assert.AreEqual(2, displayTicket.dishes.Count);
        Assert.IsTrue(displayTicket.dishes.ContainsKey("pizza"));
        Assert.IsTrue(displayTicket.dishes.ContainsKey("chips"));
        Assert.IsFalse(displayTicket.dishes.ContainsKey("water"));
    }


   
}
