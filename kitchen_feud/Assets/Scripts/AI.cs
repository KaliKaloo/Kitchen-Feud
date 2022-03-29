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
        Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers","Agent"),GameSetup.GS.spawnPoints1[0].position,Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
