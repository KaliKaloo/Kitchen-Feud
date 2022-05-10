using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Photon.Pun;

using UnityEngine.UI;

public class Owner : MonoBehaviour
{
    public int team;
    public Animator anim;
    public bool playOnce;
    public GameObject oven;
    private GameObject localPlayer;
    private NavMeshAgent agent;
    public bool currentlyTalking;
    public bool firstTime;
    public bool currentlyShouting;
    public bool returned;
    public bool collected;
    public bool collecting;
    private bool toCollect;
    public bool stopLeaning;
    private bool returningToKitchen;
    private bool following;
    private bool calledName;
    private bool notThrowing;
    private bool shouting;
    private bool inKitchenSecondTime;
    public GameObject Owner1;
    public GameObject Owner2;
    public GameObject keyboard;
    public GameObject mouse;
    public GameObject playerToFollow;
    public TextMeshProUGUI Text;
    private bool faceforward;
    private static GlobalTimer timer = new GlobalTimer();
    public ParseScore scores = new ParseScore();
    public bool shout;
    public PhotonView PV;
    private int localPlayerID;
    public bool goThrowSmokeBomb;
    private bool throwNow;
    public bool thrownSmokeBomb;
    public Owner otherOwner;
    public Vector3 spawnPoint;
    public Vector3 kitchenDestinationPoint;
    public AudioSource audioSource;
    private bool decided;
    

    private System.Random rnd = new System.Random();

    private void Awake()
    {
        Owner1 = AI.Instance.owner1Avatar;
        Owner2 = AI.Instance.owner2Avatar;
        keyboard = AI.Instance.keyBoard;
        mouse = AI.Instance.mouse;

      
    }
    void Start()
    {
        PV = GetComponent<PhotonView>();
        localPlayerID = (int)PhotonNetwork.LocalPlayer.CustomProperties["ViewID"];
        localPlayer = PhotonView.Find(localPlayerID).gameObject;
        audioSource = GetComponent<AudioSource>();

        keyboard = AI.Instance.keyBoard;
        mouse = AI.Instance.mouse;

        anim = GetComponent<Animator>();


      Text =   PhotonView.Find(205).GetComponentInChildren<TextMeshProUGUI>();
        agent = GetComponent<NavMeshAgent>();
        if (PhotonNetwork.IsMasterClient)
        {
            if (team == 1)
            {
                oven = GameObject.Find("Oven1");
                spawnPoint = GameSetup.GS.OSP1.position;
                kitchenDestinationPoint = new Vector3(12.61f, 0.2f, -4.8f);
                agent.SetDestination(kitchenDestinationPoint);

            }
            else if (team == 2)
            {
                oven = GameObject.Find("Oven2");
                spawnPoint = GameSetup.GS.OSP2.position;
                kitchenDestinationPoint = new Vector3(-6.363f, 0.2f, -7);

                agent.SetDestination(kitchenDestinationPoint);

            }
        }



    }

    void Update()
    {
        //Master client controls all owner actions
        if (PhotonNetwork.IsMasterClient)
        {
            //only execute if NavMeshAgent Component is enabled
            if (agent.enabled) {
            //assign the other owner
            if (!otherOwner)
            {
               assignOtherOwners();
            }
            //Control Animations for both owners, i.e. when they should be talking, shouting etc.
            controllingAnimationsForBothTeams();

            if (team == 1)
            {
              //make sure owner faces the right way when in kitchen
              correctingAgentRotationTOne();
              //carry out the owner's initial actions.
              ownerOneInitialAction();
            }
            else if (team == 2)
            {
                    //make sure owner faces the right way when in kitchen
                    correctingAgentRotationTOne();
                    //carry out the owner's initial actions.
                    ownerTwoInitialAction();

            }

                //Enter Kitchen Second Time
                enterSecondTime();


                if (shout == true)

                {
                    findPlayerToShout();
                }
    
                if (playerToFollow)
                {

                    followPlayer();
                }
            }


            //Collect dish from oven after a certain time has passed
            if (!collected && oven.transform.Find("ovencanvas(Clone)") && timer.GetLocalTime() < timer.GetTotalTime()/6)
            {
                if (oven.GetComponentInChildren<Timer>().timer == 5)
                {
                    collectFromOven();
                    collecting = true;
                    collected = true;
                }

            }

            if (collecting)
            {
                //Pressing Collect on OvenCanvas
                pressingCollectOnOven();

            }
            //someone collects oven dish before Owner reaches the oven
                if (collecting && !oven.transform.Find("ovencanvas(Clone)"))
                {
                        PV.RPC("setText", RpcTarget.All, PV.ViewID, "Ahh, got there before me!");
                        returnNormally();
                         collecting = false;

                }
            }
        }
    


    public Photon.Realtime.Player getLowestCookedDishesByTeam(int team)
    {
        string property = "CookedDishes";
        int lowest = 0;
        string name = "";
        Photon.Realtime.Player p = null;
        if (team == 1)
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties[property] != null && (int)player.CustomProperties[property] <= lowest && (int)player.CustomProperties["Team"] == team && 
                    PhotonView.Find((int)player.CustomProperties["ViewID"]).GetComponent<PlayerVoiceManager>().entered1)
                {
                    lowest = (int)player.CustomProperties[property];
                    name = player.NickName;
                    p = player;
                }
            }
        }else if(team == 2)
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {

                if (player.CustomProperties[property] != null && (int)player.CustomProperties[property] <= lowest && (int)player.CustomProperties["Team"] == team &&
                    PhotonView.Find((int)player.CustomProperties["ViewID"]).GetComponent<PlayerVoiceManager>().entered2)
                {
                    lowest = (int)player.CustomProperties[property];
                    name = player.NickName;
                    p = player;
                }
            }
        }
        return p;
    }

    void pressingCollectOnOven()
    {
        if (agent.remainingDistance < Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete &&
                   (agent.transform.position - oven.transform.position).sqrMagnitude < 4)
        {
            if (oven.transform.Find("ovencanvas(Clone)"))
            {

                oven.GetComponentInChildren<OvenFire>().GetComponentInChildren<exitOven>().TaskOnClick();
                PV.RPC("setText", RpcTarget.All, PV.ViewID, "I collected the dish from the Oven!");
                returnNormally();
                collecting = false;

            }
        }
    }

    void enterSecondTime() {
        if(timer.GetLocalTime() == 190)
        //if (timer.GetLocalTime() == timer.GetTotalTime() / 4)
        {
            PV.RPC("showOwner", RpcTarget.All, PV.ViewID);

            agent.SetDestination(kitchenDestinationPoint);
            inKitchenSecondTime = true;
            stopLeaning = false;

        }
        if (inKitchenSecondTime)
        {
            if ((transform.position - kitchenDestinationPoint).magnitude < 1)
            {
                StartCoroutine(waitBeforeShouting());
                inKitchenSecondTime = false;

            }
        }
    }




    void followPlayer()
    {

        if ((agent.transform.position - playerToFollow.transform.position).sqrMagnitude < 3 * 3)
        {

            agent.transform.LookAt(playerToFollow.transform);

            if (!shouting)
            {
                anim.SetBool("IsShouting", true);
                int netScore = 0;
                if (team == 1)
                {
                    netScore = scores.GetScore1() - scores.GetScore2();
                }
                else if (team == 2)
                {
                    netScore = scores.GetScore2() - scores.GetScore1();
                }

                if (netScore > -80 && netScore < 0)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "You've cooked the least amount of dishes! We're losing by a small margin, I think you should go sabotage!");
                }
                else if (netScore < -80)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "You've cooked the least amount of dishes! Can you please stop being lazy, or at least go sabotage");
                }
                else if (netScore < 150 && netScore > 0)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "You've cooked the least amount of dishes! We need to increase the gap between our scores!");

                }
                else if (netScore > 150)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "You've cooked the least amount of dishes! We're winning but we can't get lazy!");

                }
                else if (netScore == 0)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "You've cooked the least amount of dishes! We're drawing! We can't afford laziness!");

                }

                StartCoroutine(talking());
                shouting = true;
            }
            if (shouting && !currentlyTalking)
            {
                anim.SetBool("IsShouting", false);
                stopLeaning = false;

                returnWithHeadShake();


                calledName = false;
                shout = false;
                following = false;
                shouting = false;
                playerToFollow = null;


            }
        }
    }

    void ownerTwoInitialAction()
    {
        if (otherOwner.notThrowing)
        {
            stopLeaning = true;
            shout = true;
            otherOwner.notThrowing = false;
        }


        if (otherOwner.thrownSmokeBomb)
        {
            if (rnd.Next(2) == 0)
            {
                stopLeaning = true;
                StartCoroutine(startShouting());
                goThrowSmokeBomb = true;



            }
            else
            {
                stopLeaning = true;
                StartCoroutine(startShouting());

                PV.RPC("setText", RpcTarget.All, PV.ViewID, "Arghh! We can't let them do this, Can someone please throw a smoke bomb in their kitchen too!");
                StartCoroutine(leavingKitchen());
                returned = true;


            }
            otherOwner.thrownSmokeBomb = false;
        }
        if (goThrowSmokeBomb)
        {
            throwSmokeBomb();
            throwNow = true;
            goThrowSmokeBomb = false;
        }
        if (throwNow)
        {

            if ((agent.transform.position - new Vector3(6.743f, 0.2f, 2.076f)).magnitude < 1)
            {
                GetComponent<SmokeGrenade>().UseSmokeOwner(2);
                throwNow = false;
                //ADD TEXT SO OWNER SENDS MESSAGE TO OTHER TEAM
                agent.ResetPath();
                agent.SetDestination(new Vector3(-6.363f, 0.2f, -7));
                thrownSmokeBomb = true;
                returningToKitchen = true;

            }
        }
        if (returningToKitchen)
        {
            if ((transform.position - new Vector3(-6.363f, 0.2f, -7)).magnitude < 1)
            {
                StartCoroutine(leavingKitchen());
                returningToKitchen = false;
                returned = true;
            }
        }
        if (returned)
        {
            if ((transform.position - spawnPoint).magnitude < 3)
            {
                // transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
                PV.RPC("hideOwner", RpcTarget.All, PV.ViewID);
                returned = false;
            }
        }
    }
    void findPlayerToShout()
    {
        if (!following)
        {
            Photon.Realtime.Player p = null;
            if (team == 1) {
                p = getLowestCookedDishesByTeam(1);
            }else if(team == 2) {
                p = getLowestCookedDishesByTeam(2);
            }



            if (p != null)
            {
                playerToFollow = PhotonView.Find((int)p.CustomProperties["ViewID"]).gameObject;
            }
            if (playerToFollow)
            {

                agent.SetDestination(playerToFollow.transform.position - new Vector3(1, 0, 1));
                if (!calledName)
                {
                    if (p != null)
                    {
                        PV.RPC("setText", RpcTarget.All, PV.ViewID, p.NickName + "!");
                    }
                    //Text.text = p.NickName + "!";
                    calledName = true;
                }
            }
            else
            {
                if (timer.GetLocalTime() == timer.GetTotalTime() * 3 / 8)
                //if (timer.GetLocalTime() < 260)
                {
                    firstTime = true;

                    shout = false;
                }
            }

        }
    }


    void ownerOneInitialAction()
    {

        if(timer.GetLocalTime() == 270 && !decided)
        //if (timer.GetLocalTime() == timer.GetTotalTime() / 2 - 15 && !decided)
        {
            // if (timer.GetLocalTime() == timer.GetTotalTime() / 4 - 10)
            if (rnd.Next(2) == 1)

            {
                stopLeaning = true;
                goThrowSmokeBomb = true;
            }
            else
            {
                stopLeaning = true;

                shout = true;
                notThrowing = true;
            }
            decided = true;
            //else do something
        }



        if (goThrowSmokeBomb)
        {
            throwSmokeBomb();
            throwNow = true;
            goThrowSmokeBomb = false;

        }
        if (throwNow)
        {

            if ((transform.position - new Vector3(-13.3f, 0.2f, 3.6f)).magnitude < 1)
            {
                GetComponent<SmokeGrenade>().UseSmokeOwner(1);
                stopLeaning = false;
                throwNow = false;
                //ADD TEXT SO OWNER SENDS MESSAGE TO OTHER TEAM
                agent.ResetPath();
                agent.SetDestination(new Vector3(12.61f, 0.2f, -4.8f));
                thrownSmokeBomb = true;
                returningToKitchen = true;

            }
        }
        if (returningToKitchen)
        {
            if ((transform.position - (new Vector3(12.61f, 0.2f, -4.8f))).magnitude < 1)
            {
                StartCoroutine(leavingKitchen());
                returningToKitchen = false;
                returned = true;
            }
        }
        if (returned)
        {
            if ((transform.position - spawnPoint).magnitude < 1)
            {
                PV.RPC("hideOwner", RpcTarget.All, PV.ViewID);
                //  transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
                returned = false;
            }
        }

    }

    void correctingAgentRotationTOne()
    {
        if((agent.transform.position - kitchenDestinationPoint).magnitude < 1)
      //  if (agent.transform.position.x > 12 && agent.transform.position.z < -4 && agent.remainingDistance == 0)
        {
            if (!faceforward)
            {
                int net = 0;
                if(team == 1)
                {
                    net = scores.GetScore1() - scores.GetScore2();
                }else if (team == 2)
                {
                    net = scores.GetScore2() - scores.GetScore1();

                }

                if (net == 0)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "We're drawing. We need to step up our game if we want to get the edge over them!");


                }
                else if (net > 0)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "We're winning! Keep it up guys!");


                }
                else if (net < 0)
                {
                    PV.RPC("setText", RpcTarget.All, PV.ViewID, "We're losing! We need to stop being lazy and push if we want to win");


                }
                StartCoroutine(talking());
                faceforward = true;

            }


            if (anim.GetBool("IsShakingHead"))
            {
                anim.SetBool("IsShakingHead", false);
            }
            if (!stopLeaning && !currentlyShouting && !currentlyTalking)
            {
                if (!anim.GetBool("IsLeaning"))
                {
                    anim.SetBool("IsLeaning", true);
                }
            }
            if (firstTime)
            {
                StartCoroutine(leavingKitchen());
                firstTime = false;
            }

            if (transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
    }


    void controllingAnimationsForBothTeams()
    {
        if (!currentlyTalking && anim.GetBool("IsTalking"))
        {
            anim.SetBool("IsTalking", false);
        }
        if (currentlyTalking && !anim.GetBool("IsTalking"))
        {
            if (anim.GetBool("IsLeaning"))
            {
                anim.SetBool("IsLeaning", false);
            }
            anim.SetBool("IsTalking", true);
        }
        if (!currentlyShouting && anim.GetBool("IsShouting"))
        {
            anim.SetBool("IsShouting", false);
        }
        if (currentlyShouting && !anim.GetBool("IsShouting"))
        {
            if (anim.GetBool("IsLeaning"))
            {
                anim.SetBool("IsLeaning", false);
            }
            anim.SetBool("IsShouting", true);
        }
        if (!playOnce && (currentlyShouting || currentlyTalking))
        {
            StartCoroutine(playSounds());
            playOnce = true;
        }
        if (stopLeaning)
        {
            if (anim.GetBool("IsLeaning"))
            {
                anim.SetBool("IsLeaning", false);
            }
        }
    }

    void assignOtherOwners()
    {
        if (team == 1)
        {
            if (AI.Instance.Owner1)
            {
                otherOwner = AI.Instance.Owner2.GetComponent<Owner>();
            }
        }
        else if (team == 2)
        {
            if (AI.Instance.Owner1)
            {
                otherOwner = AI.Instance.Owner1.GetComponent<Owner>();
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


    private IEnumerator waitBeforeShouting()
    {
        yield return new WaitForSeconds(5);
        stopLeaning = true;
        shout = true;
    }
    private IEnumerator leavingKitchen()
    {
        
        yield return new WaitForSeconds(8);
        StartCoroutine(talking());

        if (team == 1)
        {
            PV.RPC("setText", RpcTarget.All, PV.ViewID, "Alright guys, I'm going to go away, I'll return soon, we need to win!");
            agent.SetDestination(spawnPoint);
        }
        else if(team == 2)
        {
            PV.RPC("setText", RpcTarget.All, PV.ViewID, "Keep going guys, I'll be back to check on you.");
            agent.SetDestination(spawnPoint);
        }
        returned = true;

    }

    void returnWithHeadShake()
    {
       //if (timer.GetLocalTime() > timer.GetTotalTime()/4)
       if(timer.GetLocalTime() > 210)
        {
            firstTime = true;
        }
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

    public void returnNormally()
    {
        agent.ResetPath();
        agent.SetDestination(new Vector3(12.61f, 0.2f, -4.8f));
    }
    public void returnNormally2()
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
            PV.RPC("setText", RpcTarget.All, PV.ViewID, "Arghh, we can't let them do this! I'm going to throw a smoke bomb too!");
            agent.SetDestination(new Vector3(6.743f, 0.2f, 2.076f));

        }
    }
    public IEnumerator playSounds()
    {
        PV.RPC("playOwner", RpcTarget.All, PV.ViewID);
        yield return new WaitForSeconds(3);
        playOnce = false;
    }
    [PunRPC]
    void setText(int viewID, string message)
    {
        Owner o = PhotonView.Find(viewID).GetComponent<Owner>();
       int playerID = (int)PhotonNetwork.LocalPlayer.CustomProperties["ViewID"];

        PlayerVoiceManager pVM = PhotonView.Find(playerID).GetComponent<PlayerVoiceManager>();
        if (team == 1)
        {
            if (pVM.entered1 && pVM.GetComponent<PhotonView>().IsMine)
            {
                o.keyboard.SetActive(false);
                o.mouse.SetActive(false);
                o.Owner2.SetActive(false);
                o.Owner1.SetActive(true);
                o.Text.text = message;
            }
        }else if(team == 2)
        {
            if (pVM.entered2 && pVM.GetComponent<PhotonView>().IsMine)
            {
                o.keyboard.SetActive(false);
                o.mouse.SetActive(false);
                o.Owner1.SetActive(false);
                o.Owner2.SetActive(true);
                o.Text.text = message;
            }
        }

    }

    [PunRPC]
    void hideOwner(int ViewID)
    {
        PhotonView.Find(ViewID).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;

    }
    [PunRPC]
    void showOwner(int ViewID)
    {
        PhotonView.Find(ViewID).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;

    }
    [PunRPC]
    void playOwner(int ViewID)
    {
        PhotonView.Find(ViewID).GetComponent<Owner>().audioSource.Play();
    }

}
