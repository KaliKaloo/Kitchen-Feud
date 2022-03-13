using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class MovingInstructions : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private GameObject LocalPlayer;
    public GameObject mushroom1;
    public GameObject mushroom2;
    private float time = 1.5f;
    public float timer;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        Text.text = "Welcome! You can now move around using WASD or Arrow Keys!";
        timer = time;
   
    }

    // Update is called once per frame
    void Update()
    {
        if (LocalPlayer == null)
        {
            LocalPlayer = GameObject.Find("Local");
        }
        if (Text.text == "Welcome! You can now move around using WASD or Arrow Keys!" && (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            Text.text = "Nice! Feel free look up and down by holding right click";
            
        }
        else if (Text.text == "Nice! Feel free look up and down by holding right click" && Input.GetMouseButtonDown(1))
        {
            Text.text = "Brilliant! You can also pick up items using left click!";
        }
        
        else if (Text.text == "Brilliant! You can also pick up items using left click!" &&
            LocalPlayer.transform.GetChild(2).childCount != 0) {
            Text.text = "Nicely Done! You can also drop items by using left click!";


        }else if (Text.text == "Nicely Done! You can also drop items by using left click!" &&
            LocalPlayer.transform.GetChild(2).childCount == 0)
        {
            Text.text = "Great! You're all set now";
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
