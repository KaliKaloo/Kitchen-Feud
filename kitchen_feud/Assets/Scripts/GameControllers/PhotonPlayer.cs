using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    GameObject Team1;
    GameObject Team2;

    // Start is called before the first frame update
    void Start()
    {
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
        if (myAvatar == null && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] != 0)
        {
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
            {
                if (PV.IsMine)
                {
                    int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints1.Length);
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "cat_playerModel"),
                        GameSetup.GS.spawnPoints1[spawnPicker].position, Quaternion.identity);
                    PV.RPC("syncMat", RpcTarget.All, myAvatar.GetComponent<PhotonView>().ViewID, "cat_Red");
                    //Material newMat = Resources.Load("cat_Red", typeof(Material)) as Material;
                    //myAvatar.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
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
                    PV.RPC("syncMat", RpcTarget.All, myAvatar.GetComponent<PhotonView>().ViewID, "cat_Blue");
                   // Material newMat = Resources.Load("cat_Blue", typeof(Material)) as Material;
                   // myAvatar.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
                    Team1.SetActive(false);


                }
            }
        }
    }



    [PunRPC]
    void syncMat(int viewID,string name)
    {
        Material newMat = Resources.Load(name, typeof(Material)) as Material;
        PhotonView.Find(viewID).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;

    }
}