using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDrag : MonoBehaviour
{
    float speed = 5.0f, yaw = 0.0f, pitch = 0.0f;

    Quaternion currentRotation;
    Vector3 currentEulerAngles;

    void Start()
    {
        ResetCamera = Camera.main.transform.eulerAngles;
        Debug.Log(ResetCamera);
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButton(1)) //start dragging
        {
            yaw += speed * Input.GetAxis("Mouse X");
            pitch -= speed * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            // transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
            // currentRotation.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            // transform.rotation = currentRotation;
        }

        if (Input.GetMouseButton(2)){
            transform.localRotation = Quaternion.identity;

        }
    }
}