using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDrag : MonoBehaviour
{
    float speed = 5.0f, yaw = 0.0f, pitch = 0.0f;
    private Vector3 camReset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButton(1)) //start dragging
        {
            camReset = transform.rotation.eulerAngles;
            yaw += speed * Input.GetAxis("Mouse X");
            pitch -= speed * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        if (Input.GetMouseButton(2)){
            transform.eulerAngles = new Vector3(0.0f,0.0f,0.0f);
            yaw = 0.0f;
            pitch = 0.0f;
            Debug.Log(transform.eulerAngles);
        }
    }
}