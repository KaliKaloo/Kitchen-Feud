using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class globalClicked{
    public static bool clicked = false;

    public static bool applianceInteract = false;
    public static bool trayInteract = false;
    public static bool enterEnemyKitchen = false;

}

public class MovingInstructions : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private GameObject LocalPlayer;
    public GameObject mushroom1;
    public GameObject mushroom2;
    private float time = 13f;
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
            Text.text = "Brilliant! You can also pick up items inside the subrooms using left click!";
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
            // textWriter.writeText();
        }
        else if (Text.text == "Great, now collect the other ingredients and click on the appliance again to start cooking!" && LocalPlayer.transform.GetChild(2).childCount == 1 )
        {
            Debug.Log("here");
            if(LocalPlayer.transform.GetChild(2).GetChild(0).CompareTag("Dish")){
                Debug.Log("now here");

                Text.text = "Put the dish on the white tray that has the correct order number";
                textWriter.writeText2("Then click on the tray to serve, or make more dishes",2f);
            }
            
        }
        // final initial instruction
        else if (Text.text == "Then click on the tray to serve, or make more dishes" &&  globalClicked.trayInteract)
        {
            Text.text = "Remember that you're against another resturant. You can go to their kitchen and sabotage them!";
            textWriter.writeText2("", 3f);
        }
        else if (Text.text == "" &&  globalClicked.enterEnemyKitchen)
        {
            Text.text = "In the enemy's kitchen, you can throw smoke bombs, steal items or cook using their appliances for double points!";
            textWriter.writeText2("Be careful, they can kick you out by clicking on your player",3f);
            InitializeTimer();
            started = true;
           
        }

        if (started == true)
        {
            Decrement();


            if (timer < 0)
            {
                gameObject.SetActive(false);
                started = false;
            }
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
