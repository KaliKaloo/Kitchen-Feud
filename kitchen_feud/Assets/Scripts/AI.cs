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
    private int totalTime = timer.GetTotalTime();
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
                //if (timer.GetLocalTime() == timer.GetTotalTime() / 2 && !ownersSpawned){
                if(timer.GetLocalTime() == 298 && !ownersSpawned) { 
                    Owner1 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Owner_cat_Model"), (GameSetup.GS.OSP1.position), Quaternion.identity);
                    Owner2 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Owner_panda_Model"), (GameSetup.GS.OSP2.position), Quaternion.identity);
                    ownersSpawned = true;
                }
            }
            else
            {
                if (timer.GetLocalTime() == timer.GetTotalTime() -1 && !ownersSpawned){
                    Owner1 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Owner_cat_Model"), (GameSetup.GS.OSP1.position), Quaternion.identity);
                    Owner2 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Owner_panda_Model"), (GameSetup.GS.OSP2.position), Quaternion.identity);
                    ownersSpawned = true;
                }
            }
            if (GameObject.Find("Local") && GameObject.FindGameObjectsWithTag("Waiter1").Length < 3)
            {
                if (GameObject.FindGameObjectsWithTag("Waiter1").Length == 0)
                {
                    Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Team1Waiter"), (GameSetup.GS.WSP1[0].position), Quaternion.identity);
                    agentsT1.Add(Agent);
                    Agent.GetComponent<PhotonView>().RPC("setAgentName",RpcTarget.All,Agent.GetPhotonView().ViewID, "Waiter1");
                }
                else if (GameObject.FindGameObjectsWithTag("Waiter1").Length == 1)
                {
                    Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Team1Waiter"), GameSetup.GS.WSP1[1].position, Quaternion.identity);
                    Agent.GetComponent<NavMeshAgent>().Warp(new Vector3(GameSetup.GS.WSP1[1].position.x, GameSetup.GS.WSP1[1].position.y, GameSetup.GS.WSP1[1].position.z));
                    agentsT1.Add(Agent);
                    Agent.GetComponent<PhotonView>().RPC("setAgentName",RpcTarget.All,Agent.GetPhotonView().ViewID, "Waiter2");

                }
                else if (GameObject.FindGameObjectsWithTag("Waiter1").Length == 2)
                {

                    Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Team1Waiter"), GameSetup.GS.WSP1[2].position, Quaternion.identity);
                    Agent.GetComponent<NavMeshAgent>().Warp(new Vector3(GameSetup.GS.WSP1[2].position.x, GameSetup.GS.WSP1[2].position.y, GameSetup.GS.WSP1[2].position.z));
                    agentsT1.Add(Agent);
                    Agent.GetComponent<PhotonView>().RPC("setAgentName",RpcTarget.All,Agent.GetPhotonView().ViewID, "Waiter3");

                }
            }

            if (GameObject.Find("Local") && GameObject.FindGameObjectsWithTag("Waiter2").Length < 2)
            {
                if (GameObject.FindGameObjectsWithTag("Waiter2").Length == 0)
                {

                    Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Team2Waiter"), GameSetup.GS.WSP2[0].position, Quaternion.identity);
                    Agent.GetComponent<NavMeshAgent>().Warp(new Vector3(GameSetup.GS.WSP2[0].position.x, GameSetup.GS.WSP2[0].position.y, GameSetup.GS.WSP2[0].position.z));
                    agentsT2.Add(Agent);
                    Agent.GetComponent<PhotonView>().RPC("setAgentName",RpcTarget.All,Agent.GetPhotonView().ViewID, "Waiter1");

                }else if (GameObject.FindGameObjectsWithTag("Waiter2").Length == 1)
                {

                    Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Team2Waiter"), GameSetup.GS.WSP2[1].position, Quaternion.identity);
                    Agent.GetComponent<NavMeshAgent>().Warp(new Vector3(GameSetup.GS.WSP2[1].position.x, GameSetup.GS.WSP2[1].position.y, GameSetup.GS.WSP2[1].position.z));
                    agentsT2.Add(Agent);
                    Agent.GetComponent<PhotonView>().RPC("setAgentName",RpcTarget.All,Agent.GetPhotonView().ViewID, "Waiter2");

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

                //Agent.GetPhotonView().TransferOwnership(1000);

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

                            if (!a.GetComponent<Agent>().tray && a.transform.GetChild(2).childCount == 0&&a.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
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

                //Agent.GetPhotonView().TransferOwnership(1000);

            }

            if (GameObject.Find("SP21").GetComponent<Serving>().used)
            {
                GameObject[] SP2s = GameObject.FindGameObjectsWithTag("ServingPoint2");
                for (int i = 0; i < SP2s.Length;i++)
                {
                    SP2s[i].GetComponent<Serving>().used = false;
                }
            }
            if (GameObject.Find("Serving Point (3)").GetComponent<Serving>().used)
            {
                GameObject[] SP1s = GameObject.FindGameObjectsWithTag("ServingPoint1");
                for (int i = 0; i < SP1s.Length;i++)
                {
                    SP1s[i].GetComponent<Serving>().used = false;

                }
            }

            
        }
        }
    }

