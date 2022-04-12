using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int playersCount;
    GameObject Team1;
    GameObject Team2;
    public Hashtable scene = new Hashtable();
    public bool SceneLoaded;

    // Start is called before the first frame update
    void Start()
    {
        SceneLoaded = false;
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
          //  PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
            Team1 = GameObject.FindGameObjectWithTag("Team1");
            Team2 = GameObject.FindWithTag("Team2");
            
        }
    }

    // Update is called once per frame
    void Update()
    
    
    {
      

        if ( PhotonNetwork.LocalPlayer.CustomProperties["loaded"] == null  || (int) PhotonNetwork.LocalPlayer.CustomProperties["loaded"] != 1)
        {
            if (!myAvatar && (int) PhotonNetwork.LocalPlayer.CustomProperties["Team"] != 0)
            {
                if ((int) PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
                {
                    if (PV.IsMine)
                    {

                        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints1.Length);
                        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "cat_playerModel"),
                            GameSetup.GS.spawnPoints1[spawnPicker].position, Quaternion.identity);
                        Team2.SetActive(false);
                    }
                }
                else
                {
                    if (PV.IsMine)
                    {
                        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints2.Length);
                        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "cat_playerModel"),
                            GameSetup.GS.spawnPoints2[spawnPicker].position, Quaternion.identity);
                        Team1.SetActive(false);


                    }
                }
            }


            scene["loaded"] = 1;
            PhotonNetwork.LocalPlayer.SetCustomProperties(scene);
            //}
        }




    }
}