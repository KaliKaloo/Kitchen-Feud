using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class Owner : MonoBehaviour
{
    public int team;
    private Animator anim;
    public GameObject oven;

    private NavMeshAgent agent;

    private bool collected;
    private bool toCollect;
    private bool following;
    private bool shouting;
    private GameObject playerToFollow;
    private Vector3 playerToFollowPos;
    public TextMeshProUGUI Text;
    public UITextWriter writer;
    private bool faceforward;
    private static GlobalTimer timer = new GlobalTimer();
    public ParseScore scores = new ParseScore();
    public bool shout;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       
        
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
        
       
        if (team == 1)
        {
            if (agent.transform.position.x > 12 && agent.transform.position.z < -4 && agent.remainingDistance == 0 &&
                agent.transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                if (anim.GetBool("IsShakingHead"))
                {
                    anim.SetBool("IsShakingHead", false);
                }
                
                agent.transform.rotation = Quaternion.Euler(0, 0, 0);

            }
            if (!faceforward && agent.transform.position.x > 12 && agent.transform.position.z < -4 && agent.remainingDistance ==  0)
            {
                agent.transform.rotation = Quaternion.Euler(0, 0, 0);
                faceforward = true;
                anim.SetBool("IsTalking",true);

                if (scores.GetScore1() == scores.GetScore2())
                {
                    Text.text = "We're drawing. We need to step up our game if we want to get the edge over them!";

                }else if(scores.GetScore1() > scores.GetScore2())
                {
                    Text.text = "We're winning! Keep it up guys!";

                }else if (scores.GetScore1() < scores.GetScore2())
                {
                    Text.text = "We're losing! We need to stop being lazy and push if we want to win";

                }
                writer.writeText();
                
            }

            if (!writer.writing && anim.GetBool("IsTalking"))
            {
                anim.SetBool("IsTalking",false);
            }
            
        }
        if(timer.GetLocalTime() == 280)
        {
            shout = true;
        }
        if(shout == true)
        {

            foreach (Photon.Realtime.Player p in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if((int)p.CustomProperties["CookedDishes"] == 0)
                {
                    if (!agent.hasPath & !following)
                    {
                        playerToFollow = PhotonView.Find((int)p.CustomProperties["ViewID"]).gameObject;
                        
                        agent.SetDestination(playerToFollow.transform.position - new Vector3(1,0,1));
                        //Text.text = p.NickName + "!";
                        break;
                    }
                }
             
            }
            if((agent.transform.position - playerToFollow.transform.position).sqrMagnitude < 2 * 2)
            {
                agent.transform.LookAt(playerToFollow.transform);

                if (!shouting)
                {
                    anim.SetBool("IsShouting", true);
                    Text.text = "You haven't cooked a single dish! I think you should go help sabotage";
                    writer.writeText();
                    shouting = true;
                }
                //Debug.LogError(writer.writing);
                if(shouting && !writer.writing) {

                    following = true;
                    agent.ResetPath();

                    returnWithHeadShake();
                    Debug.LogError("Stopped");
                    anim.SetBool("IsShouting", false);
                   
                    shout = false;
                    shouting = false;

                    
                }
            
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
    void returnWithHeadShake()
    {
        Debug.LogError(agent.pathStatus);
        agent.SetDestination(new Vector3(12.61f, 0.2f, -4.8f));
        //agent.SetDestination(new Vector3(0,0,0));
        anim.SetBool("IsShakingHead", true);
    }
    
}
