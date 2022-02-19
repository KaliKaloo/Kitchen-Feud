using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class DisplayTicket : MonoBehaviour
{
    public Text orderNumberText;
    public Text orderMainText;
    public Text orderSideText;
    public Text orderDrinkText;

    public string orderid;
    public int orderNumber;

    //method to update the UI
    public void SetUI(Order o)
    {
        orderNumberText.text = o.orderNumber.ToString();
        int size = o.dishes.Count;
        orderMainText.text = o.dishes[0].name;
        if (size > 1) orderSideText.text = o.dishes[1].name;
        else if (size == 3) orderDrinkText.text = o.dishes[2].name;

        orderid = o.orderID;
        


    }
   
}
