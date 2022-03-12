using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class DisplayTicket : MonoBehaviour
{
    public Text orderNumberText;
   
    public TextMeshProUGUI orderMainText;
    public TextMeshProUGUI orderSideText;
    public TextMeshProUGUI orderDrinkText;
    public Dictionary<string, Sprite> dishes = new Dictionary<string, Sprite>();

    public string orderid;
    public int orderNumber;

    //method to update the UI
    public void SetUI(Order o)
    {
        orderNumberText.text = o.orderNumber.ToString();
        int size = o.dishes.Count;
        
        orderMainText.text = o.dishes[0].name;
        if (size > 1) {
            orderSideText.text = o.dishes[1].name;
        }
        else if (size == 3) {
            orderDrinkText.text = o.dishes[2].name;
        }
        foreach (BaseFood d in o.dishes){
            if (dishes.ContainsKey(d.name) != true)
            {
                dishes.Add(d.name, d.img);
            }
        }
        orderid = o.orderID;
    }

    [PunRPC]
    void SetToF(int viewID)
    {
        PhotonView.Find(viewID).gameObject.SetActive(false);
    }
    [PunRPC]
    void clearAll()
    {
        this.orderDrinkText.text = "";
        this.orderMainText.text = "";
        this.orderNumberText.text = "";
        //this.orderSideText.text = "";
        //this.dishes.Clear();
    }
}

