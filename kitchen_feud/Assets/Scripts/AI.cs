using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public GameObject Agent;
    public List<GameObject> agents = new List<GameObject>();
    public PhotonView PV;
    
   
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (GameObject.Find("Local") && GameObject.FindGameObjectsWithTag("Agent").Length < 3)
            {
                if (GameObject.FindGameObjectsWithTag("Agent").Length == 0)
                {
                    Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Agent"), (GameSetup.GS.WSP1[0].position), Quaternion.identity);
                    agents.Add(Agent);
                    //Agent.GetComponent<PhotonView>().TransferOwnership(1000);
                    //Agent.GetComponent<NavMeshAgent>().Warp(new Vector3(GameSetup.GS.WSP1[0].position.x, GameSetup.GS.WSP1[0].position.y, GameSetup.GS.WSP1[0].position.z));
                }
                else if (GameObject.FindGameObjectsWithTag("Agent").Length == 1)
                {
                    Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Agent"), GameSetup.GS.WSP1[1].position, Quaternion.identity);
                    Agent.GetComponent<NavMeshAgent>().Warp(new Vector3(GameSetup.GS.WSP1[1].position.x, GameSetup.GS.WSP1[1].position.y, GameSetup.GS.WSP1[1].position.z));
                    agents.Add(Agent);
                    // Agent.GetComponent<PhotonView>().TransferOwnership(1000);

                }
                else if (GameObject.FindGameObjectsWithTag("Agent").Length == 2)
                {

                    Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Agent"), GameSetup.GS.WSP1[2].position, Quaternion.identity);
                    Agent.GetComponent<NavMeshAgent>().Warp(new Vector3(GameSetup.GS.WSP1[2].position.x, GameSetup.GS.WSP1[2].position.y, GameSetup.GS.WSP1[2].position.z));
                    agents.Add(Agent);
                    // Agent.GetComponent<PhotonView>().TransferOwnership(1000);

                }
            }

            if (agents.Count == 3)
            {
                foreach (Transform ts in GameObject.Find("k1").transform.Find("Trays"))
                {
                    //Check if Tray is ready to be collected and doesn't already have a waiter assigned
                    if (ts.GetComponent<Tray>().isReady && !ts.GetComponent<Tray>().Agent)
                    {


                        foreach (GameObject a in agents)
                        {

                            if (!a.GetComponent<Agent>().tray && a.transform.GetChild(2).childCount == 0)
                            {
                                //Assign Tray to waiter
                                a.GetComponent<PhotonView>().RPC("setTray", RpcTarget.All, a.GetComponent<PhotonView>().ViewID,
                                    ts.GetComponent<PhotonView>().ViewID);
                                //Assign waiter to Tray
                                ts.GetComponent<PhotonView>().RPC("setAgent", RpcTarget.All, ts.GetComponent<PhotonView>().ViewID,
                                a.GetComponent<PhotonView>().ViewID);

                                break;

                            }

                        }

                    }
                }

                //Agent.GetPhotonView().TransferOwnership(1000);

            }
        }
        }
    }

