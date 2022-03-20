using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		transform.LookAt(transform.position + cam.forward);
    }
}
