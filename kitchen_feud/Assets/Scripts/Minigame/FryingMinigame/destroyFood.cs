using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class destroyFood : MonoBehaviour
{
    public GameObject pan;
    
    private void Update()
    {
        if (GameObject.Find("Pan"))
        {
            pan = GameObject.Find("Pan");
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FriedFoodController>())
        {
            PhotonView PV = collision.GetComponent<PhotonView>();
            pan.GetComponent<PanController>().foodInstancesCounter += 1;
            if (collision.GetComponent<FriedFoodController>().onPlate == false)
            {
                PV.RPC("destP", RpcTarget.All, PV.ViewID);
            }
            

           
        }
    }

}
