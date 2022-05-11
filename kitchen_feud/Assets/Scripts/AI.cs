using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class AI : MonoBehaviour
{
    public GameObject Agent;
    public List<GameObject> agentsT1 = new List<GameObject>();
    public List<GameObject> agentsT2 = new List<GameObject>();
    public PhotonView PV;
    private bool ownersSpawned;
    private static GlobalTimer timer = new GlobalTimer();
    public GameObject Owner1;
    public GameObject Owner2;
    public GameObject keyBoard;
    public GameObject mouse;
    public GameObject owner1Avatar;
    public GameObject owner2Avatar;
    public static AI Instance;


    private void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        owner1Avatar.SetActive(false);
        owner2Avatar.SetActive(false);

    }

    void Update()
    {


        if (PhotonNetwork.IsMasterClient && GameObject.FindGameObjectsWithTag("Player").Length == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (SceneManager.GetActiveScene().name != "kitchens Test")
            {
                if (timer.GetLocalTime() == timer.GetTotalTime() / 2 && !ownersSpawned)
                {
                    Owner1 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Owner_cat_Model"), (GameSetup.GS.OSP1.position), Quaternion.identity);
                    Owner2 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Owner_panda_Model"), (GameSetup.GS.OSP2.position), Quaternion.identity);
                    ownersSpawned = true;
                }
            }
            else
            {
                if (timer.GetLocalTime() == timer.GetTotalTime() - 1 && !ownersSpawned)
                {
                    Owner1 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Owner_cat_Model"), (GameSetup.GS.OSP1.position), Quaternion.identity);
                    Owner2 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Owner_panda_Model"), (GameSetup.GS.OSP2.position), Quaternion.identity);
                    ownersSpawned = true;
                }
            }

            if (PhotonNetwork.LocalPlayer.CustomProperties["ViewID"] != null)
            {
                if ((int)PhotonNetwork.LocalPlayer.CustomProperties["ViewID"] != 0)
                {
                    if (GameObject.Find("Local") && agentsT1.Count < 3)
                    {
                        createTeamOneWaiters();
                    }

                    if (GameObject.Find("Local") && agentsT2.Count < 2)
                    {

                        createTeamTwoWaiters();

                    }
                }
            }
            if (agentsT1.Count == 3)
            {
                foreach (Transform ts in GameObject.Find("k1").transform.Find("Trays"))
                {
                    //Check if Tray is ready to be collected and doesn't already have a waiter assigned
                    if (ts.GetComponent<Tray>().isReady && !ts.GetComponent<Tray>().Agent)
                    {


                        foreach (GameObject a in agentsT1)
                        {

                            if (!a.GetComponent<Agent>().tray && a.transform.GetChild(2).childCount == 0 && a.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
                            {
                                //Assign Tray to waiter
                                a.GetComponent<Agent>().tray = ts.GetComponent<Tray>();
                                //Assign waiter to Tray
                                ts.GetComponent<Tray>().Agent = a;

                                break;

                            }

                        }

                    }
                }


            }

            if (agentsT2.Count == 2)
            {
                foreach (Transform ts in GameObject.Find("k2").transform.Find("Trays"))
                {
                    //Check if Tray is ready to be collected and doesn't already have a waiter assigned
                    if (ts.GetComponent<Tray>().isReady && !ts.GetComponent<Tray>().Agent)
                    {


                        foreach (GameObject a in agentsT2)
                        {

                            if (!a.GetComponent<Agent>().tray && a.transform.GetChild(2).childCount == 0 && a.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
                            {
                                //Assign Tray to waiter
                                a.GetComponent<Agent>().tray = ts.GetComponent<Tray>();


                                //Assign waiter to Tray
                                ts.GetComponent<Tray>().Agent = a;

                                break;

                            }

                        }

                    }
                }


            }

            if (GameObject.Find("SP21").GetComponent<Serving>().used)
            {
                GameObject[] SP2s = GameObject.FindGameObjectsWithTag("ServingPoint2");
                for (int i = 0; i < SP2s.Length; i++)
                {
                    SP2s[i].GetComponent<Serving>().used = false;
                }
            }
            if (GameObject.Find("Serving Point (3)").GetComponent<Serving>().used)
            {
                GameObject[] SP1s = GameObject.FindGameObjectsWithTag("ServingPoint1");
                for (int i = 0; i < SP1s.Length; i++)
                {
                    SP1s[i].GetComponent<Serving>().used = false;

                }
            }


        }
    }

    void createTeamOneWaiters()
    {
        for (int i = 0; i < 3; i++)
        {
            Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Team1Waiter"), (GameSetup.GS.WSP1[i].position), Quaternion.identity);
            agentsT1.Add(Agent);
            Agent.GetComponent<PhotonView>().RPC("setAgentName", RpcTarget.All, Agent.GetPhotonView().ViewID, "Waiter" + (i + 1).ToString());
        }
    }

    void createTeamTwoWaiters()
    {
        for (int i = 0; i < 2; i++)
        {
            Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Team2Waiter"), GameSetup.GS.WSP2[i].position, Quaternion.identity);
            Agent.GetComponent<NavMeshAgent>().Warp(new Vector3(GameSetup.GS.WSP2[i].position.x, GameSetup.GS.WSP2[i].position.y, GameSetup.GS.WSP2[i].position.z));
            agentsT2.Add(Agent);
            Agent.GetComponent<PhotonView>().RPC("setAgentName", RpcTarget.All, Agent.GetPhotonView().ViewID, "Waiter" + (i + 1).ToString());
        }

    }



}

