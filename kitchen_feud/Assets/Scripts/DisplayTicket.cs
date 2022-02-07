using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayTicket : MonoBehaviour
{
    public Text orderNumberText;
    public Text orderMainText;
    public Text orderSideText;
    public Text orderDrinkText;

    public void DisplayNewRandomOrder()
    {
       SetUI(Database.GetRandomOrder());
    }

    //method to update the UI
    public void SetUI(Order o)
    {
        orderNumberText.text = o.orderNumber.ToString();
        orderMainText.text = o.orderMain;
        orderSideText.text = o.orderSide;
        orderDrinkText.text = o.orderDrink;
    }
}
