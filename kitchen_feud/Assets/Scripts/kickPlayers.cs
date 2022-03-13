using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class kickPlayers : MonoBehaviour
{
    public GameObject[] players;
    public GameObject kickCanvas;
    public List<GameObject> oPlayers;
    public int otherTeam;
    public PhotonView PV;
    public bool enteredOne = false;
    public bool enteredTwo = false;
    public List<int> oPl1;
    public List<int> oPl2;
    public bool noneIn = true;
    // Start is called before the first frame update
    void Start()
    {
        kickCanvas.SetActive(false);
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
                    
                  //  Debug.LogError(players.Length);
                    if (p.GetComponent<PlayerController>().myTeam != GameObject.Find("Local").GetComponent<PlayerController>().myTeam)
                    {
                      
                        oPlayers.Add(p);
                       // Debug.LogError("Name: " + p.name + " Team: " + p.GetComponent<PlayerController>().myTeam);
                    }
                }

            }

            if (GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 1 )
            {
                foreach (GameObject p in oPlayers)
                {

                    if (Vector3.Distance(p.transform.position, new Vector3(-3.28f, 1.09f, -14.94f)) < 10 && Vector3.Distance(GameObject.Find("Local").transform.position, new Vector3(-3.28f, 1.09f, -14.94f)) < 10)
                    {
                        noneIn = false;
                        Debug.Log(p.transform.position);
                    }
                    else
                    {
                        
                        noneIn = true;
                    }
                }
            }else if (GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 2 )
                {
                foreach (GameObject p in oPlayers)
                {

                    if (Vector3.Distance(p.transform.position, new Vector3(-3.22f, 1.09f, 9.4f)) < 10 && Vector3.Distance(GameObject.Find("Local").transform.position, new Vector3(-3.22f, 1.09f, 9.4f)) < 10)
                    {
                        noneIn = false;
                    }
                    else
                    {
                        noneIn = true;
                    }
                }
            }
            
            
            if (noneIn)
            {
                Debug.LogError("NoneIN" + noneIn);
                kickCanvas.SetActive(false);
            }

        }

         if(enteredOne == true && GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 1 && Vector3.Distance(GameObject.Find("Local").transform.position, new Vector3(-3.28f, 1.09f, -14.94f) )<10)
        {
            kickCanvas.SetActive(true);
        }
         else if (enteredTwo == true && GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 2 && Vector3.Distance(GameObject.Find("Local").transform.position, new Vector3(-3.22f, 1.09f, 9.4f)) < 10)
        {
            kickCanvas.SetActive(true);
        }




    }

    public void kickPlayer()
    {
        //Debug.LogError(oPlayers[0].name);
        //oPlayers[0].transform.position = new Vector3(20.51f,1f,2.34f);
        if (GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 1)
        {
            PhotonView.Find(oPl1[0]).GetComponent<PhotonView>().RPC("synctele", RpcTarget.All, PhotonView.Find(oPl1[0]).GetComponent<PhotonView>().ViewID, new Vector3(4.13f, 0.006363153f, 7.16f));
            PV.RPC("removeOp", RpcTarget.All, PV.ViewID, oPl1[0],1);
        }
        else
        {
            PhotonView.Find(oPl2[0]).GetComponent<PhotonView>().RPC("synctele", RpcTarget.All, PhotonView.Find(oPl2[0]).GetComponent<PhotonView>().ViewID, new Vector3(-1.98f, 0.006363153f, -8.37f));
            PV.RPC("removeOp", RpcTarget.All, PV.ViewID, oPl2[0], 2);
        }
    }
   
    [PunRPC]
    void addOp(int viewID1,int viewID, int team)
    {
        if(team == 1)
        {
            PhotonView.Find(viewID1).GetComponent<kickPlayers>().oPl1.Add(viewID);
           // oPl1.Add(viewID);
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
            // oPl1.Add(viewID);
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
}
