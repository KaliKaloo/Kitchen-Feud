using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrayText : MonoBehaviour
{
    public TextMeshProUGUI myText;

    public string GetText()
    {
        return myText.text;
    }

    public void ChangeText(string newText)
    {
        myText.text = "Order: " + newText;
    }
}
