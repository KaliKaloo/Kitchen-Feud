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

    void LateUpdate()
    {

        if (transform.name != "Nametag(Clone)")
        {
            Debug.LogError(transform.name);
            transform.LookAt(transform.position + cam.forward);

        }
        else
        {
            Debug.LogError("J");
            if (!GetComponent<PhotonView>().IsMine)
            {
                Debug.LogError(transform.parent.parent.name);
                cam = GameObject.Find("Local").transform.GetChild(3);
                Debug.LogError("Done");
                transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
            }
        }
        }
    }

