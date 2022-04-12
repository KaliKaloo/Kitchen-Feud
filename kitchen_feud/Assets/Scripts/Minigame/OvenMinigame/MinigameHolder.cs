using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MinigameHolder : MonoBehaviour
{
    // Start is called before the first frame update
    [PunRPC]
    void PlayFireSound(int viewID) {
        PhotonView.Find(viewID).gameObject.GetComponent<AudioSource>().Play();
    }
}
