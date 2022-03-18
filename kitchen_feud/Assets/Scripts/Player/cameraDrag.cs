using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cameraDrag : MonoBehaviour
{
    public float rotatespeed;

    public Transform playerBody;
    float xRotation = 0.0f;
    float mouseX;
    float mouseY;

    void Update()
    {
      /*  if (GetComponentInParent<PhotonView>().IsMine)
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                 mouseX = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
                 mouseY = Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 48f);
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);


            }
            else
            {
                Cursor.lockState = CursorLockMode.None; ;
            }

        }
      */
    }
 
  
}