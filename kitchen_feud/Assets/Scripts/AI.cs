using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class AI : MonoBehaviour
{
    public GameObject Agent;
    public GameObject Owner;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Owner = Agent = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(0,0,0), Quaternion.identity);
            //Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Agent"), GameSetup.GS.spawnPoints1[0].position, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient && !GameObject.Find("Agent(Clone)"))
        {
            Agent = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Agent"), GameSetup.GS.spawnPoints1[0].position, Quaternion.identity);
        }
        //Agent.GetPhotonView().TransferOwnership(1000);
    }
}
