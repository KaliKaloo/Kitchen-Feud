using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Dish : MonoBehaviour
{
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


}



