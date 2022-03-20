using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class cameraDrag : MonoBehaviour
{
    public float rotatespeed;
	Slider rotateSlider;
    public Transform playerBody;
    float xRotation = 0.0f;
    public Rigidbody rb;
    public PhotonView PV;
    float Horizontal;
    float Vertical;
    Vector3 movement;
    // float yaw;
    public float speed;

    private void Start()
    {
        rotateSlider = GameObject.Find("Rotation").GetComponentInChildren<Slider>();
        rb = GetComponentInParent<Rigidbody>();
        PV = GetComponentInParent<PhotonView>();
    }
    private void Update()
    {
        rotatespeed = rotateSlider.value;

        if (PV.IsMine)
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            movement = transform.forward * Vertical + transform.right * Horizontal;
            // if (Input.GetMouseButton(1))
            // {
            //     yaw = (yaw + Input.GetAxis("Mouse X") * speed) % 360f;
            // }
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
                xRotation -= mouseY * speed;
                xRotation = Mathf.Clamp(xRotation, -90f, 48f);
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up*mouseX);

            }
            else
            {
                Cursor.lockState = CursorLockMode.None; ;
            }
        }

    }

    private void FixedUpdate()
    {
        if (PV.IsMine)
        {
            {
                // rb.rotation = Quaternion.Euler(new Vector3(0f, yaw, 0f));
                rb.MovePosition(rb.position + movement * 5 * Time.fixedDeltaTime);
            }
        }
    }

}

