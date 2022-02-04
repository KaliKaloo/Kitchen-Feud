using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CreateOrderObject : MonoBehaviour
{
    public Text orderNumberText;
    public Text orderMainText;
    public Text orderSideText;
    public Text orderDrinkText;

    void Start() {
       /* public void GetNewRandomOrder()
        {*/
            //on button click set ui to be a random order from the orderDB
            SetUI(Database.GetRandomOrder());
        //}
    }
    //method to update the UI
    public void SetUI(Order o)
    {
        if (o == null)
        {
            orderNumberText.text = null;
            orderMainText.text = null;
            orderSideText.text = null;
            orderDrinkText.text = null;
        }

        orderNumberText.text = o.orderNumber.ToString();
        orderMainText.text = o.orderMain;
        orderSideText.text = o.orderSide;
        orderDrinkText.text = o.orderDrink;
    }
}
