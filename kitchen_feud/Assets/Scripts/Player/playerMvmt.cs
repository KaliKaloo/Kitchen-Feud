using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class playerMvmt : MonoBehaviour
{
    public float rotatespeed;
    public float mvmtSpeed;
	GameObject rotateSlider;
	GameObject speedSlider;
    public Transform playerBody;
    float xRotation = 0.0f;
    public Rigidbody rb;
    public PhotonView PV;
    float Horizontal;
    float Vertical;
    Vector3 movement;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        PV = GetComponentInParent<PhotonView>();
    }


    private void Update()
    {

        if (PV.IsMine)
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            movement = transform.forward * Vertical + transform.right * Horizontal;
          
        }
        rotateSlider = GameObject.Find("Rotation");
        speedSlider = GameObject.Find("Speed");
        if (rotateSlider && speedSlider){
            mvmtSpeed = speedSlider.GetComponentInChildren<Slider>().value;
            rotatespeed = rotateSlider.GetComponentInChildren<Slider>().value;
        }

    }
    private void LateUpdate()
    {

        if (PV.IsMine)
        {
            if (Input.GetMouseButton(1))
            {

                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 48f);
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up*mouseX);

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }

    private void FixedUpdate()
    {
        if (PV.IsMine)
        {
            {
                rb.MovePosition(rb.position + movement * mvmtSpeed * Time.fixedDeltaTime);
            }
        }
    }

}

