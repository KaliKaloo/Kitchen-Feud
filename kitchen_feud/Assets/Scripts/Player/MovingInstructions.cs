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
    public static bool enemyInstructions = false;

    public static bool holdingFireEx = false;


}

public class MovingInstructions : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private GameObject LocalPlayer;

    private float time = 4f;
    public float timer;
    private bool started = false;
    private bool finished = false;
    private bool enemyInstructions = false;

    // how long in seconds enemy instructions have between when shown
    private float enemyInstructionDelay = 6;
    List<string> randomInstructionList = new List<string>
        {
            "TIP: Steal their ingredients and dishes!", "TIP: Throw a smoke bomb by pressing the button in the bottom right", "TIP: Cook in their kitchen for double points!", "TIP: Add fake 10 seconds to their oven timer! This might cause a fire!", "TIP: Be careful, they can kick you out!"
        };

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
            
        }
        else if (Text.text == "Nice! Feel free look up and down by holding right click and dragging" && Input.GetMouseButtonDown(1))
        {
            Text.text = "Brilliant! You can also pick up items inside the rooms using left click!";

        }
        
        else if (Text.text == "Brilliant! You can also pick up items inside the rooms using left click!" &&
            LocalPlayer.transform.GetChild(2).childCount != 0) {
            Text.text = "Nicely Done! You can drop items by clicking again anywhere";



        }else if (Text.text == "Nicely Done! You can drop items by clicking again anywhere" &&
            LocalPlayer.transform.GetChild(2).childCount == 0)
        {
            Text.text = "Click on the dishes on the top left tickets to see the recipe";
            
        }
       
        else if (Text.text == "Click on the dishes on the top left tickets to see the recipe" &&
            globalClicked.clicked)
        {
            Text.text = "The colours on the recipe card match the subrooms. It's where the ingredients are located. Collect the first ingredient and put them on the glowing appliance";
        }
        else if (Text.text == "The colours on the recipe card match the subrooms. It's where the ingredients are located. Collect the first ingredient and put them on the glowing appliance" && globalClicked.applianceInteract )
        {
            Text.text = "Great, now collect the other ingredients and click on the appliance again to start cooking!";
        }
        else if (Text.text == "Great, now collect the other ingredients and click on the appliance again to start cooking!" && LocalPlayer.transform.GetChild(2).childCount == 1 )
        {
            if(LocalPlayer.transform.GetChild(2).GetChild(0).CompareTag("Dish")){

                Text.text = "Put the dish on the white tray that has the correct order number. Then click on the tray to serve, or make more dishes";
            }
            
        }
        // final initial instruction
        else if (Text.text == "Put the dish on the white tray that has the correct order number. Then click on the tray to serve, or make more dishes" &&  globalClicked.trayInteract)
        {
            Text.text = "Remember that you're against another resturant. You can go to their kitchen and sabotage them!";
            InitializeTimer();
            started = true;

        }
        if (globalClicked.enterEnemyKitchen)
        {
            // locks coroutine from being spammed after entered enemy kitchen
            if (!globalClicked.enemyInstructions && randomInstructionList.Count > 0){
                StartCoroutine(EnemyKitchenInstructions());
            }
        }
        

        if (globalClicked.holdingFireEx){
            Text.text = "Tip: Press 'F' to use the fire extinguisher";
            globalClicked.holdingFireEx = false;
            InitializeTimer();
            started = true;
        }

        if (started == true)
        {
            Decrement();


            if (timer < 0)
            {
                started = false;
                Text.text ="";  
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

    // randomly selects from a list of instructions when enter enemy kitchen
    private IEnumerator EnemyKitchenInstructions()
    {
        globalClicked.enemyInstructions = true;

        // show a random instruction from list then remove it
        
        int first = Random.Range(0, randomInstructionList.Count);
        Text.text = randomInstructionList[first];
        randomInstructionList.RemoveAt(first);
        
        yield return new WaitForSeconds(enemyInstructionDelay);

        Text.text = "";

        // // show a random instruction from remaining list then remove it
        // int second = Random.Range(0, 3);
        // Text.text = randomInstructionList[second];
        // randomInstructionList.RemoveAt(second);

        // yield return new WaitForSeconds(enemyInstructionDelay);

        // // show a last remaining instruction
        // Text.text = randomInstructionList[0];


    }

}
