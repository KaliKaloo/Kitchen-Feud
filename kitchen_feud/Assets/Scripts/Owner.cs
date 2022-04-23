using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class Owner : MonoBehaviour
{
    public int team;
    private Animator anim;
    public GameObject oven;

    private NavMeshAgent agent;

    private bool collected;
    public TextMeshProUGUI Text;
    public UITextWriter writer;
    private Text Score1;
    private Text Score2;
    private bool faceforward;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Score1 = GameObject.Find("Score1").GetComponent<Text>();
        Score2 = GameObject.Find("Score2").GetComponent<Text>();
        
        writer = GameObject.Find("instruction type").GetComponentInChildren<UITextWriter>();
        Text = GameObject.Find("instruction type").GetComponentInChildren<TextMeshProUGUI>();
        agent = GetComponent<NavMeshAgent>();
        if (team == 1)
        {
            oven = GameObject.Find("Oven1");
            agent.SetDestination(new Vector3(12.61f,0.2f,-4.8f));

        }
        else if(team == 2)
        {
            oven = GameObject.Find("Oven2");
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(anim.GetBool("IsTalking"));
       
        if (team == 1)
        {
            if (!faceforward && agent.transform.position.x > 12 && agent.transform.position.z < -4 && agent.remainingDistance ==  0)
            {
                agent.transform.rotation = Quaternion.Euler(0, 0, 0);
                faceforward = true;
                anim.SetBool("IsTalking",true);
                if (Int32.Parse(Score1.text) == Int32.Parse(Score2.text))
                {
                    Text.text = "We're drawing. We need to step up our game if we want to get the edge over them!";

                }
                writer.writeText();
                
            }
        }
        
        /*if (!collected  && oven.GetComponent<Appliance>().minigameCanvas)
        {
            collectFromOven();
            collected = true;
        }
        

        if (agent.remainingDistance < Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete &&
            Math.Abs(agent.transform.position.x - oven.transform.position.x) < 1 && Math.Abs(agent.transform.position.z - oven.transform.position.z) < 2)
        {
            Debug.LogError("Hello");
            oven.GetComponent<Appliance>().minigameCanvas.GetComponentInChildren<exitOven>().TaskOnClick();
            agent.GetComponent<PlayerHolding>().pickUpItem(oven.GetComponent<Appliance>().cookedDish);
        }*/
    }

    void collectFromOven()
    {
        Vector3 ovenDestination;
        Vector3 ovenPosition;
  

        ovenPosition = oven.transform.position;
        ovenDestination = new Vector3(ovenPosition.x , ovenPosition.y,ovenPosition.z - 1.6f);

        if (!agent.hasPath)
        {
            agent.SetDestination(ovenDestination);
        }
        
        

}
    
}
