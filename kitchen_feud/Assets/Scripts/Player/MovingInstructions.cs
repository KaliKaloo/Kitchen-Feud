using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class globalClicked{
    public static bool clicked = false;

    public static bool applianceInteract = false;
    public static bool trayInteract = false;
}

public class MovingInstructions : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private GameObject LocalPlayer;
    public GameObject mushroom1;
    public GameObject mushroom2;
    private float time = 2.5f;
    public float timer;
    private bool started = false;
    private bool finished = false;
    public UITextWriter textWriter;

    void Start()
    {
        Text.text = "Welcome! You can now move around using WASD or Arrow Keys!";
        timer = time;
   
    }

    void Update()
    {
        if (LocalPlayer == null)
        {
            LocalPlayer = GameObject.Find("Local");
        }
        if (Text.text == "Welcome! You can now move around using WASD or Arrow Keys!" && (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            Text.text = "Nice! Feel free look up and down by holding right click and dragging";
            textWriter.writeText();
            
        }
        else if (Text.text == "Nice! Feel free look up and down by holding right click and dragging" && Input.GetMouseButtonDown(1))
        {
            // Text.text = "Brilliant! You can also pick up items inside the subrooms using left click!";
            Text.text = "Great, now collect the other ingredients and click on the appliance again to start cooking!";

            textWriter.writeText();

        }
        
        else if (Text.text == "Brilliant! You can also pick up items inside the subrooms using left click!" &&
            LocalPlayer.transform.GetChild(2).childCount != 0) {
            Text.text = "Nicely Done! You can drop items by clicking again anywhere";
            textWriter.writeText();



        }else if (Text.text == "Nicely Done! You can drop items by clicking again anywhere" &&
            LocalPlayer.transform.GetChild(2).childCount == 0)
        {
            Text.text = "Click on the dishes on the top left tickets to see the recipe";
            textWriter.writeText();

            
            
        }
        else if (Text.text == "Nicely Done! You can drop items by clicking again anywhere" &&
            LocalPlayer.transform.GetChild(2).childCount == 0)
        {
            Text.text = "Click on the dishes on the top left tickets to see the recipe";
            textWriter.writeText();
        }
        else if (Text.text == "Click on the dishes on the top left tickets to see the recipe" &&
            globalClicked.clicked)
        {
            // first instruction
            Text.text = "The colours on the recipe card match the subrooms. It's where the ingredients are located.";
            // second instruction shows after 2 seconds (see UI text writer script for delay)
            textWriter.writeText2("Collect the first ingredient and put them on the glowing appliance", 2f);
        }
        else if (Text.text == "Collect the first ingredient and put them on the glowing appliance" && globalClicked.applianceInteract )
        {
            Text.text = "Great, now collect the other ingredients and click on the appliance again to start cooking!";
            textWriter.writeText();
        }
        else if (Text.text == "Great, now collect the other ingredients and click on the appliance again to start cooking!" && LocalPlayer.transform.GetChild(2).childCount == 1 )
        {
            if(LocalPlayer.transform.GetChild(2).GetChild(0).CompareTag("Dish")){
                Text.text = "Put the dish on the white tray that has the correct order number";
                textWriter.writeText2("Then click on the tray to serve, or make more dishes",2f);
            }
            
        }
        // final initial instruction
        else if (Text.text == "Then click on the tray to serve, or make more dishes" &&  globalClicked.trayInteract)
        {
            Text.text = "Remember that you're against another resturant.";
            textWriter.writeText2("Do your best!", 2f);
        }
    }
   
    public void Decrement()
    {
        timer -= Time.deltaTime;
    }

    public void InitializeTimer()
    {
        timer = time;
    }
}
