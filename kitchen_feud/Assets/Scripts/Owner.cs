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
    private GameObject localPlayer;
    private NavMeshAgent agent;
    private bool currentlyTalking;
    private bool currentlyShouting;
    private bool currentlyTalking2;
    public bool collected;
    public bool collecting;
    private bool toCollect;
    private bool following;
    private bool calledName;
    private bool shouting;
    public GameObject Owner1;
    public GameObject Owner2;
    public GameObject keyboard;
    public GameObject mouse;
    public GameObject playerToFollow;
    private Vector3 playerToFollowPos;
    public TextMeshProUGUI Text;
    private bool faceforward;
    private static GlobalTimer timer = new GlobalTimer();
    public ParseScore scores = new ParseScore();
    public bool shout;
    public PhotonView PV;
    private int localPlayerID;
    private bool goThrowSmokeBomb;
    private bool throwNow;
    private System.Random rnd = new System.Random();
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.LogError(AI.Instance.owner1Avatar.name);
        Owner1 = AI.Instance.owner1Avatar;
        Owner2 = AI.Instance.owner2Avatar;
      
    }
    void Start()
    {
        PV = GetComponent<PhotonView>();
        localPlayerID = (int)PhotonNetwork.LocalPlayer.CustomProperties["ViewID"];
        localPlayer = PhotonView.Find(localPlayerID).gameObject;
        //if (team == 1)
        //{
        //    Owner1 = GameObject.Find("Owner1");
        //    Owner1.SetActive(false);
            


        //}
        //else if(team == 2)
        //{
        //    Owner2 = GameObject.Find("Owner2");
        //    Owner2.SetActive(false);


        //}
        keyboard = GameObject.Find("keyboard controls");
        mouse = GameObject.Find("mouse controls");

        anim = GetComponent<Animator>();
       
        
        Text = GameObject.Find("instruction type").GetComponentInChildren<TextMeshProUGUI>();
        agent = GetComponent<NavMeshAgent>();
        if (PhotonNetwork.IsMasterClient)
        {
            if (team == 1)
            {
                oven = GameObject.Find("Oven1");
                agent.SetDestination(new Vector3(12.61f, 0.2f, -4.8f));

            }
            else if (team == 2)
            {
                oven = GameObject.Find("Oven2");
                agent.SetDestination(new Vector3(-6.363f, 0.2f, -7));

            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (!currentlyTalking && anim.GetBool("IsTalking"))
            {
                anim.SetBool("IsTalking", false);
            }
            if (currentlyTalking && !anim.GetBool("IsTalking"))
            {
                anim.SetBool("IsTalking", true);
            }
            if (!currentlyShouting && anim.GetBool("IsShouting"))
            {
                anim.SetBool("IsShouting", false);
            }
            if (currentlyShouting && !anim.GetBool("IsShouting"))
            {
                anim.SetBool("IsShouting", true);
            }

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
                if (!faceforward && agent.transform.position.x > 12 && agent.transform.position.z < -4 && agent.remainingDistance == 0)
                {
                    agent.transform.rotation = Quaternion.Euler(0, 0, 0);
                    faceforward = true;
                    anim.SetBool("IsTalking", true);

                    if (scores.GetScore1() == scores.GetScore2())
                    {
                        Debug.LogError("INSTRUCTIONS MAYVE??");
                        PV.RPC("setText", RpcTarget.All, PV.ViewID, "We're drawing. We need to step up our game if we want to get the edge over them!");
                        //Text.text = "We're drawing. We need to step up our game if we want to get the edge over them!";

                    }
                    else if (scores.GetScore1() > scores.GetScore2())
                    {
                        PV.RPC("setText", RpcTarget.All, PV.ViewID, "We're winning! Keep it up guys!");

                        //Text.text = "We're winning! Keep it up guys!";

                    }
                    else if (scores.GetScore1() < scores.GetScore2())
                    {
                        PV.RPC("setText", RpcTarget.All, PV.ViewID, "We're losing! We need to stop being lazy and push if we want to win");

                       // Text.text = "We're losing! We need to stop being lazy and push if we want to win";

                    }
                    StartCoroutine(talking());

                }

               // if(rnd.Next(1) == 1)
                {
                   // if(timer.GetLocalTime() == timer.GetTotalTime()/4 - 10)
                    if(timer.GetLocalTime() == 280)
                    {
                        goThrowSmokeBomb = true;
                    }
                }

             

                if (goThrowSmokeBomb)
                {
                    throwSmokeBomb();
                    throwNow = true;
                    goThrowSmokeBomb = false;
                }
                if (throwNow)
                {
               
                    if((agent.transform.position - new Vector3(-13.3f, 0.2f,3.6f)).magnitude<1)
                    {
                        GetComponent<SmokeGrenade>().UseSmokeOwner(1);
                        throwNow = false;
                        //ADD TEXT SO OWNER SENDS MESSAGE TO OTHER TEAM
                        agent.ResetPath();
                        agent.SetDestination(new Vector3(12.61f, 0.2f, -4.8f));

                    }
                }
        

            }
            else if (team == 2)
            {
                if (agent.transform.position.x < -6f && agent.transform.position.z < -5.7f && agent.remainingDistance == 0 &&
                agent.transform.rotation != Quaternion.Euler(0, 0, 0))
                {
                    if (anim.GetBool("IsShakingHead"))
                    {
                        anim.SetBool("IsShakingHead", false);
                    }

                    agent.transform.rotation = Quaternion.Euler(0, 0, 0);

                }
                if (!faceforward && agent.transform.position.x < -6f && agent.transform.position.z < -5.7f && agent.remainingDistance == 0)
                {
                    //Debug.LogError("Hello");
                    agent.transform.rotation = Quaternion.Euler(0, 0, 0);
                    faceforward = true;
                    anim.SetBool("IsTalking", true);

                    if (scores.GetScore1() == scores.GetScore2())
                    {
                        PV.RPC("setText2", RpcTarget.All, PV.ViewID, "We're drawing. We need to step up our game if we want to get the edge over them!");

                        //Text.text = "We're drawing. We need to step up our game if we want to get the edge over them!";

                    }
                    else if (scores.GetScore1() > scores.GetScore2())
                    {
                        PV.RPC("setText2", RpcTarget.All, PV.ViewID, "We're winning! Keep it up guys!");

                        //Text.text = "We're winning! Keep it up guys!";

                    }
                    else if (scores.GetScore1() < scores.GetScore2())
                    {
                        PV.RPC("setText2", RpcTarget.All, PV.ViewID, "We're losing! We need to stop being lazy and push if we want to win");

                        //Text.text = "We're losing! We need to stop being lazy and push if we want to win";

                    }
                    StartCoroutine(talking());

                }

     
            }
            if (timer.GetLocalTime() == timer.GetTotalTime()/2 - 30)
            {
                shout = true;
            }

            if (shout == true)
            {

                foreach (Photon.Realtime.Player p in PhotonNetwork.CurrentRoom.Players.Values)
                {
                    GameObject player = PhotonView.Find((int)p.CustomProperties["ViewID"]).gameObject;
                    if ((int)p.CustomProperties["CookedDishes"] == 0 && (int)p.CustomProperties["Team"] == team)
                    {
                        if (team == 1)
                        {
                           // Debug.LogError(player.name);

                            if (player.GetComponent<PlayerVoiceManager>().entered1)
                            {
                                if (!following)
                                {
                                    playerToFollow = player;

                                    agent.SetDestination(playerToFollow.transform.position - new Vector3(1, 0, 1));
                                    //Debug.LogError("SET1");

                                    if (!calledName)
                                    {
                                        Text.text = p.NickName + "!";
                                        calledName = true;
                                    }
                                    break;
                                }
                            }
                        }
                        else if (team == 2)
                        {
                          

                            if (player.GetComponent<PlayerVoiceManager>().entered2)
                            {
                               // Debug.LogError(player.name);
                                if (!following)
                                {
                                    playerToFollow = player;

                                    agent.SetDestination(playerToFollow.transform.position - new Vector3(1, 0, 1));
                                    //Debug.LogError("SET2");

                                    if (!calledName)
                                    {
                                        Text.text = p.NickName + "!";
                                        calledName = true;
                                    }
                                    break;
                                }
                            }


                        }
                    }

                }

                if (playerToFollow)
                {
                    if ((agent.transform.position - playerToFollow.transform.position).sqrMagnitude < 2 * 2)
                    {
                        agent.transform.LookAt(playerToFollow.transform);

                        if (!shouting)
                        {
                            anim.SetBool("IsShouting", true);
                            if(team == 1)
                            {
                                PV.RPC("setText", RpcTarget.All, PV.ViewID, "You haven't cooked a single dish! I think you should go help sabotage");

                            }
                            else if(team ==2)
                            {
                                PV.RPC("setText2", RpcTarget.All, PV.ViewID, "You haven't cooked a single dish! I think you should go help sabotage");

                            }

                            // Text.text = "You haven't cooked a single dish! I think you should go help sabotage";
                            StartCoroutine(talking());
                            shouting = true;
                        }
                        if (shouting && !currentlyTalking)
                        {
                            anim.SetBool("IsShouting", false);

                            returnWithHeadShake();
                            calledName = false;
                            shout = false;
                            shouting = false;


                        }

                    }
                }


            }



            if (!collected && oven.transform.Find("ovencanvas(Clone)"))
            {
                Debug.LogError("OVENNNN");
                if (oven.GetComponentInChildren<Timer>().timer == 5)
                {
                    collectFromOven();
                    collecting = true;
                    collected = true;
                }

            }

            if (collecting)
            {

                if (agent.remainingDistance < Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete &&
                     (agent.transform.position - oven.transform.position).sqrMagnitude < 4)
                {
                    if (oven.transform.Find("ovencanvas(Clone)"))
                    {
                       
                            oven.GetComponentInChildren<OvenFire>().GetComponentInChildren<exitOven>().TaskOnClick();
                            agent.GetComponent<PlayerHolding>().pickUpItem(oven.GetComponent<Appliance>().cookedDish);
                            collecting = false;
                        

                    }
                }

            }
            if (collecting && !oven.transform.Find("ovencanvas(Clone)"))
            {
                if (team == 1)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "Ahh, got there before me!");

                    returnNormally();
                }
                else if (team == 2)
                {
                    PV.RPC("setText2", RpcTarget.All, PV.ViewID, "Ahh, got there before me!");

                    returnNormally2();
                }
                //Text.text = "Ahh, got there before me!";
                collecting = false;
            }













        }
    }

    void collectFromOven()
    {
        Vector3 ovenDestination;
        Vector3 ovenPosition;


        ovenPosition = oven.transform.position;
        ovenDestination = new Vector3(ovenPosition.x, ovenPosition.y, ovenPosition.z - 1.6f);

        if (!agent.hasPath)
        {
          
                agent.SetDestination(ovenDestination);
           
        }



    }
    private IEnumerator talking()
    {
        //keyboard.SetActive(false);
        //mouse.SetActive(false);    
        //if(team == 1)
        //{
        //    Owner1.SetActive(true);
        //}else if(team == 2)
        //{
        //    Owner2.SetActive(true);
        //}
        currentlyTalking = true;
        yield return new WaitForSeconds(3);
        currentlyTalking = false;
        
    }
    private IEnumerator startShouting()
    {

        currentlyShouting = true;
        yield return new WaitForSeconds(3);
        currentlyShouting = false;

    }


    void returnWithHeadShake()
    {
        //Debug.LogError(agent.pathStatus);
        following = true;
        agent.ResetPath();
        if (team == 1)
        {
            agent.SetDestination(new Vector3(12.61f, 0.2f, -4.8f));
        }else if(team == 2)
        {
            agent.SetDestination(new Vector3(-6.363f, 0.2f, -7));
        }
        anim.SetBool("IsShakingHead", true);
    }

    void returnNormally()
    {
        agent.ResetPath();
        agent.SetDestination(new Vector3(12.61f, 0.2f, -4.8f));
    }
    void returnNormally2()
    {
        agent.ResetPath();
        agent.SetDestination(new Vector3(-6.363f, 0.2f, -7));
    }
    void throwSmokeBomb()
    {
        StartCoroutine(startShouting());
        if (team == 1)
        {
            
            PV.RPC("setText", RpcTarget.All, PV.ViewID, "I'm gonna go over to their kitchen and throw a smoke bomb!");
            agent.SetDestination(new Vector3(-12.6f, 0.2f, 3.6f));
        }else if(team == 2)
        {
            PV.RPC("setText2", RpcTarget.All, PV.ViewID, "I'm gonna go over to their kitchen and throw a smoke bomb!");
            agent.SetDestination(new Vector3(6.743f, 0.2f, 2.076f));

        }
    }
    [PunRPC]
    void setText(int viewID, string message)
    {
        Owner o = PhotonView.Find(viewID).GetComponent<Owner>();
       int playerID = (int)PhotonNetwork.LocalPlayer.CustomProperties["ViewID"];

        PlayerVoiceManager pVM = PhotonView.Find(playerID).GetComponent<PlayerVoiceManager>();
      
        if( pVM.myTeam == 1 && pVM.entered1 && pVM.GetComponent<PhotonView>().IsMine)
        {
            o.keyboard.SetActive(false);
            o.mouse.SetActive(false);
            o.Owner1.SetActive(true);
            o.Text.text = message;
        } 

    }
    [PunRPC]
    void setText2(int viewID, string message)
    {
        Owner o = PhotonView.Find(viewID).GetComponent<Owner>();
        int playerID = (int) PhotonNetwork.LocalPlayer.CustomProperties["ViewID"];
        PlayerVoiceManager pVM = PhotonView.Find(playerID).GetComponent<PlayerVoiceManager>();
        Debug.LogError(PhotonView.Find(playerID).IsMine);
        if ( pVM.myTeam == 2 && pVM.entered2  && pVM.GetComponent<PhotonView>().IsMine)
        {
            //Debug.LogError("TESTTT");
            o.keyboard.SetActive(false);
            o.mouse.SetActive(false);
            o.Owner2.SetActive(true);
            o.Text.text = message;
        }
    }

}
