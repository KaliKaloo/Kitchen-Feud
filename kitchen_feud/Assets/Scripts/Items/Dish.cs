using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Dish : MonoBehaviour
{
    // Start is called before the first frame update
    public float points;


    [PunRPC]
    void DisableView()
    {
        Renderer r = GetComponent<Renderer>();
   
        
            r.enabled = !r.enabled;
        
    }
    [PunRPC]
    void EnView()
    {
        Renderer r = GetComponent<Renderer>();


        r.enabled = true;

    }
    [PunRPC]
    void pointSync(int point)
    {
        this.points = point;
    }
    [PunRPC]
    void setParent(int viewID,int viewID1)
    {
        PhotonView.Find(viewID).gameObject.transform.SetParent(PhotonView.Find(viewID1).gameObject.transform);
        PhotonView.Find(viewID).gameObject.transform.localPosition = Vector3.zero;
        PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = true;
        PhotonView.Find(viewID).gameObject.GetComponent<Collider>().isTrigger = false;
        PhotonView.Find(viewID).gameObject.transform.localRotation= Quaternion.Euler(Vector3.zero);
        PhotonView.Find(viewID).transform.localScale =
            PhotonView.Find(viewID).GetComponent<pickableItem>().defaultScale;

    }

}



