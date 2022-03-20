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
    bool done;
    public GameObject obj;
    public Vector3 pos;
    Quaternion xRot;
    Quaternion yRot;
    private void Start()
    {
        //  obj = transform.parent.gameObject;

        // transform.SetParent(null);

    }
   
    private void LateUpdate()
    {

        if (GetComponentInParent<PhotonView>().IsMine)
        {
            if (Input.GetMouseButton(1))
            {

                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
                xRotation -= mouseY * 3;
                xRotation = Mathf.Clamp(xRotation, -90f, 48f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

                //playerBody.Rotate(Vector3.up * mouseX);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None; ;
            }
        }

    }
 


}

