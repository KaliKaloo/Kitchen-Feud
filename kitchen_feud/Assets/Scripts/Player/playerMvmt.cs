using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    private bool disableForOthers;
    Vector3 movement;
    

    private Animator animator;


    private void Start()
    {
        GameObject parent = transform.parent.gameObject;
        rb = GetComponentInParent<Rigidbody>();
        PV = GetComponentInParent<PhotonView>();
        //SOUND -------------------------------------------------------------------
        if (GameObject.Find("Local"))
        {
            if (!GameObject.Find("Local").GetComponentInChildren<AudioListener>().enabled)
            {
                GameObject.Find("Local").GetComponentInChildren<AudioListener>().enabled = true;
            }
        }
        //-------------------------------------------------------------------------
        animator = parent.GetComponent<Animator>();

    }


    private void Update()
    {
        if (transform.parent.name != "Local" && !disableForOthers)
        {
            gameObject.SetActive(false);
            disableForOthers = true;
        }

        if (PV.IsMine && transform.parent.name =="Local")
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            movement = transform.forward * Vertical + transform.right * Horizontal;
            //SideStepping animation
            if (Vertical == 0f && Horizontal != 0)
            {
            }

            float dotProduct = Vector3.Dot(movement, transform.forward);

            // if player moving forward
            if (dotProduct > 0)
				animator.SetBool("IsMovingForwards", true);
            // if player moving backward
            else if (dotProduct < 0) 
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

        if (PV.IsMine && transform.parent.name == "Local")
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
            else if (Input.GetKey(KeyCode.Z))
            {

                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
                //xRotation -= mouseY;
                xRotation -= rotatespeed * Time.deltaTime;
                //xRotation = Mathf.Clamp(xRotation, -90f, 48f);
                //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
                playerBody.Rotate(Vector3.up*mouseX);

            }
            else if (Input.GetKey(KeyCode.C))
            {

                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
                //xRotation -= mouseY;
                xRotation += rotatespeed * Time.deltaTime;
                //xRotation = Mathf.Clamp(xRotation, -90f, 48f);
                //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
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
        if (PV.IsMine && transform.parent.name =="Local")
        {
            {
                rb.velocity =  movement * mvmtSpeed ;
            }
        }
    }

}

