using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int myTeam;
    public GameObject tmp;
    public GameObject tmp1;
    

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }
   

    }

    // Update is called once per frame
    void Update()
    {
        if (myAvatar == null && myTeam != 0)
        {
            if (myTeam == 1)
            {
                if (PV.IsMine)
                {
                    int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints1.Length);
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Player 1"),
                        GameSetup.GS.spawnPoints1[spawnPicker].position, Quaternion.identity);
                    tmp = myAvatar;
                }
            }
            else
            {
                if (PV.IsMine)
                {
                    int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints2.Length);
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Player 1"),
                        GameSetup.GS.spawnPoints2[spawnPicker].position, Quaternion.identity);
                    tmp1 = myAvatar;
                }
            }
        }
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = GameSetup.GS.nextPlayersTeam;
        GameSetup.GS.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }
    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }

}