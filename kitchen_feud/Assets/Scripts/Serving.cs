using UnityEngine;
using Photon.Pun;

public class Serving : MonoBehaviour
{
    public bool used = false;


    [PunRPC]
    void setUsed(int pointID)
    {
        PhotonView.Find(pointID).GetComponent<Serving>().used = true;
    }
    [PunRPC]
    void setUsedF(int pointID)
    {
        PhotonView.Find(pointID).GetComponent<Serving>().used = false;
    }

}
