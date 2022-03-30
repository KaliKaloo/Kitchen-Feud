using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;
using System.IO;
using TMPro;
public class nametag : MonoBehaviour
{
    private GameObject obj;
    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        
    }
    private void Update()
    {
        if (GameObject.Find("Local"))
        {
            if (GameObject.Find("Local").GetPhotonView().IsMine)
            {
                if (!GameObject.Find("Local").GetComponent<PlayerVoiceManager>().nametag)
                {
                    obj = GameObject.Find("Local");
                    obj.GetComponent<PlayerVoiceManager>().nametag = PhotonNetwork.Instantiate(Path.Combine("Healthbar", "Nametag"), obj.transform.GetChild(5).position, Quaternion.identity);
                    obj.GetComponent<PhotonView>().RPC("setNTParent", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, obj.GetComponent<PlayerVoiceManager>().nametag.GetComponent<PhotonView>().ViewID);

                }
                else if (GameObject.Find("Local").GetComponent<PlayerVoiceManager>().nametag)
                {
                    obj.GetComponent<PhotonView>().RPC("setName", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID);

                }

            }
        }
    }
  


}
