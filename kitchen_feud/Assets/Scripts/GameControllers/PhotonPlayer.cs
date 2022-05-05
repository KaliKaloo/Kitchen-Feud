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


    public Hashtable scene = new Hashtable();
    public Hashtable pViD = new Hashtable();
    public bool SceneLoaded;

    GameObject[] Team1;
    GameObject[] Team2;


    void Start()
    {
        SceneLoaded = false;
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {

            Team1 = GameObject.FindGameObjectsWithTag("Team1");
            Team2 = GameObject.FindGameObjectsWithTag("Team2");

        }
    }

    // Update is called once per frame
    void Update()
    {
   
        if (PhotonNetwork.LocalPlayer.CustomProperties["loaded"] == null ||
            (int) PhotonNetwork.LocalPlayer.CustomProperties["loaded"] != 1)
        {
            if (!myAvatar && (int) PhotonNetwork.LocalPlayer.CustomProperties["Team"] != 0)
            {

                if ((int) PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
                {

                    if (PV.IsMine)
                    {

                        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints1.Length);
                        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Player_cat_Model"),
                            GameSetup.GS.spawnPoints1[spawnPicker].position, Quaternion.identity);
                        //saving PhotonView ID of local player
                        pViD["ViewID"] = myAvatar.GetPhotonView().ViewID;
                        PhotonNetwork.LocalPlayer.SetCustomProperties(pViD);
                        foreach (GameObject obj in Team2)
                        {
                            obj.SetActive(false);

                        }
                    }
                } else
                {
                    if (PV.IsMine)
                    {
                        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints2.Length);
                        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Player_panda_Model"),
                            GameSetup.GS.spawnPoints2[spawnPicker].position, Quaternion.identity);
                        //saving PhotonView ID of local player
                        pViD["ViewID"] = myAvatar.GetPhotonView().ViewID;
                        PhotonNetwork.LocalPlayer.SetCustomProperties(pViD);


                        foreach (GameObject obj in Team1)
                        {
                            obj.SetActive(false);
                        }

                    }
                }
            }


            scene["loaded"] = 1;
            PhotonNetwork.LocalPlayer.SetCustomProperties(scene);
            //}
        }
        
    }
}