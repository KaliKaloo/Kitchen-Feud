using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class AI : MonoBehaviour
{
    public GameObject Agent;
   
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1 && PhotonNetwork.IsMasterClient && !GameObject.Find("Agent(Clone)"))
        {
            Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Agent"), GameSetup.GS.spawnPoints1[0].position, Quaternion.identity);
            Agent.GetPhotonView().TransferOwnership(1000);

        }
    }
}
