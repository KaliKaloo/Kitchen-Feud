using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDrag : MonoBehaviour
{
    public float rotatespeed = 200.0f;

    public Transform playerBody;
    float xRotation = 0.0f;

   
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;

            float mouseX =  Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
            float mouseY =  Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 48f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up*mouseX);

        }else{
            Cursor.lockState = CursorLockMode.None;;

        }

        if (Input.GetKey(KeyCode.R)){
            transform.localRotation = Quaternion.identity;
        }
    }
}