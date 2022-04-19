using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class globalClicked{
    public static bool clicked = false;

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
            textWriter.writeText2("Collect the ingredients and put them on the glowing appliances");
        }
        // final initial instruction
        else if (Text.text == "Collect the ingredients and put them on the glowing appliances" && finished)
        {
            Text.text = "done";
            textWriter.writeText();

            InitializeTimer();
            started = true;
            
        }
        if (started == true)
        {
            Decrement();


            if (timer < 0)
            {
                gameObject.SetActive(false);
               /* if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
                {
                    PhotonNetwork.Destroy(mushroom1);
                }
                else
                {
                    PhotonNetwork.Destroy(mushroom2);
                }*/
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
