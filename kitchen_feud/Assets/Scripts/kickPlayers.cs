using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;


public class kickPlayers : MonoBehaviour
{
    public GameObject[] players;
    public GameObject kickCanvas;
    public List<GameObject> oPlayers;
    public int otherTeam;
    public PhotonView PV;
    public bool enteredOne = false;
    public bool enteredTwo = false;
    public Queue q1;
    public Queue q2;
    public List<int> oPl1;
    public List<int> oPl2;
    public bool noneIn = true;
    public int playersPressing1;
    public int playersPressing2;
    public static kickPlayers Instance;


    void Awake()
    {
        Instance = this;
 
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        PV = GetComponent<PhotonView>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Length < PhotonNetwork.CurrentRoom.PlayerCount)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
         if (players.Length == PhotonNetwork.CurrentRoom.PlayerCount)
        {
          
           
            if (oPlayers.Count == 0)
            {
                
                foreach (GameObject p in players)
                {
                    
                    if (p.GetComponent<PlayerController>().myTeam != GameObject.Find("Local").GetComponent<PlayerController>().myTeam)
                    {
                      
                        oPlayers.Add(p);
                    }
                }

            }

            if (GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 1 )
            {
                if(oPl1.Count == 0)
                {
                    PV.RPC("resetPlayerPressing", RpcTarget.All, 1);
                    mouseControl.Instance.isPressed = false;
                }
                

                    if (oPl1.Count>0 && Vector3.Distance(GameObject.Find("Local").transform.position, new Vector3(-3.28f, 1.09f, -14.94f)) < 10)
                    {
                        kickCanvas.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    
                    else
                    {
                        kickCanvas.transform.GetChild(0).gameObject.SetActive(false);
                        
                    }
                
            }else if (GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 2 )
                {
                if (oPl2.Count == 0)
                {
                    PV.RPC("resetPlayerPressing", RpcTarget.All, 2);
                    mouseControl.Instance.isPressed = false;
                }
                

                    if (oPl2.Count > 0 && Vector3.Distance(GameObject.Find("Local").transform.position, new Vector3(-3.22f, 1.09f, 9.4f)) < 10)
                    {
                        kickCanvas.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        kickCanvas.transform.GetChild(1).gameObject.SetActive(false);
                    }
                
            }
            
   

        }

    }

    public void kickPlayer()
    {
        
        if(GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 1) {
            Debug.LogError(playersPressing1);
            if (playersPressing1 == 2)
                
            {

                
                    for(int i = 0; i < oPl1.Count; i++)
                    {
                        if(oPl1[i] != 0) {
                            mouseControl.Instance.isPressed = false;
                            PhotonView.Find(oPl1[i]).GetComponent<PhotonView>().RPC("synctele", RpcTarget.All, PhotonView.Find(oPl1[i]).GetComponent<PhotonView>().ViewID, new Vector3(4.13f, 0.006363153f, 7.16f));
                            PV.RPC("resetPlayerPressing", RpcTarget.All, 1);
                           
                        if (oPl1.Count == 0) {
                            kickCanvas.transform.GetChild(0).gameObject.SetActive(false);
                        }
                        

                            break;
                        }
                        
                    }
            }

        }
        else
        {
            Debug.LogError(playersPressing2);
            if (playersPressing2 == 2)

            {
            
                for (int i = 0; i < oPl2.Count; i++)
                {
                    if (oPl2[i] != 0)
                    {
                        mouseControl.Instance.isPressed = false;
                        PhotonView.Find(oPl2[i]).GetComponent<PhotonView>().RPC("synctele", RpcTarget.All, PhotonView.Find(oPl2[i]).GetComponent<PhotonView>().ViewID, new Vector3(-1.98f, 0.006363153f, -8.37f));
                        PV.RPC("resetPlayerPressing", RpcTarget.All, 2);
                        
                        if (oPl2.Count == 0)
                        {
                            kickCanvas.transform.GetChild(0).gameObject.SetActive(false);
                        }
                        break;
                    }

                }

            }
        }
        

    }

    [PunRPC]
    void addOp(int viewID1,int viewID, int team)
    {
        if(team == 1)
        {
            PhotonView.Find(viewID1).GetComponent<kickPlayers>().oPl1.Add(viewID);
       
        }
        else
        {
            PhotonView.Find(viewID1).GetComponent<kickPlayers>().oPl2.Add(viewID);
            
        }
        
    }
    [PunRPC]
    void removeOp(int viewID1, int viewID, int team)
    {
        if (team == 1)
        {
            PhotonView.Find(viewID1).GetComponent<kickPlayers>().oPl1.Remove(viewID);
            
        }
        else
        {
            PhotonView.Find(viewID1).GetComponent<kickPlayers>().oPl2.Remove(viewID);
        }

    }
    [PunRPC]
    void setEntered(int viewID, int num)
    {
        if (num == 1)
        {
            PhotonView.Find(viewID).GetComponent<kickPlayers>().enteredOne = true;
        }
        else
        {
            PhotonView.Find(viewID).GetComponent<kickPlayers>().enteredTwo = true;
        }
    }
    [PunRPC]
    void setEnteredF(int viewID, int num)
    {
        if (num == 1)
        {
            PhotonView.Find(viewID).GetComponent<kickPlayers>().enteredOne = false;
        }
        else
        {
            PhotonView.Find(viewID).GetComponent<kickPlayers>().enteredTwo = false;
        }
    }

    [PunRPC]
    void setPlayerPressing(int x,int team)
    {
        if (team == 1)
        {
            if (x == 1)
            {
                playersPressing1 += 1;
            }
            else if (x == -1)
            {
                playersPressing1 -= 1;
            }
        }
        else
        {
            if (x == 1)
            {
                playersPressing2 += 1;
            }
            else if (x == -1)
            {
                playersPressing2 -= 1;
            }
        }
    }
    [PunRPC]
    void resetPlayerPressing(int team)
    {
        if (team == 1)
        {
            playersPressing1 = 0;
        }
        else
        {
            playersPressing2 = 0;
        }
    }
}
