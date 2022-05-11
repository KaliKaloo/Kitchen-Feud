using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MinigameHolder : MonoBehaviour
{
    [PunRPC]
    void PlayFireSound(int viewID) {
        PhotonView.Find(viewID).gameObject.GetComponent<AudioSource>().Play();
    }

    [PunRPC]
    void StopFireSound(int viewID) {
        PhotonView.Find(viewID).gameObject.GetComponent<AudioSource>().Stop();
    }
}
