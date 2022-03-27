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

    private Animator animator;


    private void Start()
    {
        GameObject parent = transform.parent.gameObject;
        rb = GetComponentInParent<Rigidbody>();
        PV = GetComponentInParent<PhotonView>();
        animator = parent.GetComponent<Animator>();

    }


    private void Update()
    {

        if (PV.IsMine)
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            movement = transform.forward * Vertical + transform.right * Horizontal;
            Debug.Log(movement);

            // if player moving forward
            if (movement.z > 0)
				animator.SetBool("IsMovingForwards", true);
            // if player moving backward
            else if (movement.z < 0) 
				animator.SetBool("IsMovingBackwards", true);

            //disable movement
            else {
                // disable backwards
                if (animator.GetBool("IsMovingBackwards"))
                    animator.SetBool("IsMovingBackwards", false);
                // disable forwards
                else if (animator.GetBool("IsMovingForwards"))
				    animator.SetBool("IsMovingForwards", false);
            }
                
          
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

