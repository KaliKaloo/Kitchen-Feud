using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class ResetValues : MonoBehaviour
{
    Hashtable ht = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        ht["loaded"] = 0;
        PlayerPrefs.DeleteKey("userID");
        PlayerPrefs.Save();
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
