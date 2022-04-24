using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Billboard : MonoBehaviour
{
	public Transform cam;
    private void Start()
    {
        if (transform.parent)
        {
            cam = transform.parent.parent.GetChild(3);
            
        }
    }

    void update(){
        if (!cam){
        
            if (transform.parent)
            {
                cam = transform.parent.parent.GetChild(3);
                
            }
        }
    }
 
    void LateUpdate()
    {

        if (transform.name != "Nametag(Clone)")
        {
            transform.LookAt(transform.position + cam.forward);

        }
        else
        {
            if (!GetComponent<PhotonView>().IsMine)
            {
                if (GameObject.Find("Local"))
                {
                    cam = GameObject.Find("Local").transform.GetChild(3);
                    transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
                }
            }
        }
        }
    }

