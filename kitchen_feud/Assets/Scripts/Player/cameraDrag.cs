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
    private void Update()
    {
      
        // transform.position = obj.transform.GetChild(4).position;
        //    if(done == false)
        //    {
        //        Debug.Log("HEYYY" + transform.childCount);
        //        transform.GetChild(3).SetParent(null);
        //    }
    }
    //void Update()
    //{
    //    if (GetComponentInParent<PhotonView>().IsMine)
    //    {
    //        if (Input.GetMouseButton(1))
    //        {






    //        }
    //        else
    //        {

    //        }

    //    }

    //}
    private void FixedUpdate()
    {
        if (GetComponentInParent<PhotonView>().IsMine)
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
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
        // playerBody.Rotate (Vector3.up * mouseX);
        //playerBody.rotation = Quaternion.Euler(new Vector3(0f, mouseX, 0f));
        //   rb.rotation = Quaternion.Euler(new Vector3(0f, yaw, 0f));

        //playerBody.Rotate(Vector3.up *mouseX);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


    }
    private void LateUpdate()
    {
        //if (GetComponentInParent<PhotonView>().IsMine)
        //{
            
        //   // transform.localPosition = obj.transform.localPosition;
           
        //    if (Input.GetMouseButton(1))
        //    {
        //        //Cursor.lockState = CursorLockMode.Locked;
        //        mouseX = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
        //        mouseY = Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
        //        //xRotation -= mouseY;
        //        //xRotation = Mathf.Clamp(xRotation, -90f, 48f);
        //        yRot = Quaternion.Euler(0f, mouseX, 0f);
        //        xRot = Quaternion.Euler(-mouseY, 0f, 0f);
        //        transform.rotation = yRot * transform.rotation * xRot;

        //    }
        //    else
        //    {
        //        //Cursor.lockState = CursorLockMode.None;
        //    }
        //}
    }

}