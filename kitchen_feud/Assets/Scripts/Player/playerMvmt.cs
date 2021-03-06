using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

// allows use of current animator between scripts
public class playerAnimator
{
    public static Animator animator;

    public static void SetAnimator(Animator newAnimator)
    {
        animator = newAnimator;
    }
}

public class playerMvmt : MonoBehaviour
{
    public float rotatespeed, mvmtSpeed;
    GameObject rotateSlider, speedSlider;
    public Transform playerBody;
    float xRotation = 0.0f;
    public Rigidbody rb;
    public PhotonView PV;
    float Horizontal, Vertical;
    private bool disableForOthers;
    Vector3 movement;
    private Animator animator;


    private void Start()
    {
        GameObject parent = transform.parent.gameObject;
        rb = GetComponentInParent<Rigidbody>();
        PV = GetComponentInParent<PhotonView>();
        if (GameObject.Find("Local"))
        {
            if (!GameObject.Find("Local").GetComponentInChildren<AudioListener>().enabled)
            {
                GameObject.Find("Local").GetComponentInChildren<AudioListener>().enabled = true;
            }
        }

        animator = parent.GetComponent<Animator>();
        playerAnimator.SetAnimator(animator);

    }


    private void Update()
    {
        if (transform.parent.name != "Local" && !disableForOthers)
        {
            gameObject.SetActive(false);
            disableForOthers = true;
        }

        if (PV.IsMine && transform.parent.name == "Local")
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            movement = transform.forward * Vertical + transform.right * Horizontal;

            // animations

            // walk forward
            if (Vertical > 0)
                animator.SetBool("IsMovingForwards", true);
            else if (Vertical == 0)
                animator.SetBool("IsMovingForwards", false);

            // walk backwards
            if (Vertical < 0)
                animator.SetBool("IsMovingBackwards", true);
            else if (Vertical == 0)
                animator.SetBool("IsMovingBackwards", false);

            // strafe right
            if (Horizontal > 0)
                animator.SetBool("IsStrafingRight", true);
            else if (Horizontal == 0)
                animator.SetBool("IsStrafingRight", false);

            // strafe left
            if (Horizontal < 0)
                animator.SetBool("IsStrafingLeft", true);
            else if (Horizontal == 0)
                animator.SetBool("IsStrafingLeft", false);


        }

        updateSettings();


    }

    //mvmt and rotation settings
    private void updateSettings()
    {
        rotateSlider = GameObject.Find("Rotation");
        speedSlider = GameObject.Find("Speed");
        if (rotateSlider && speedSlider)
        {
            mvmtSpeed = speedSlider.GetComponentInChildren<Slider>().value;
            rotatespeed = rotateSlider.GetComponentInChildren<Slider>().value;
        }
    }

    private void LateUpdate()
    {
        if (PV.IsMine && transform.parent.name == "Local")
        {

            // Rotation
            if (Input.GetMouseButton(1)) //right click drag
            {
                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 48f); //clamp rotation
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);

            }
            else if (Input.GetKey(KeyCode.Q))
            {
                float rotation = rotatespeed * Time.deltaTime;
                xRotation -= rotation;
                playerBody.Rotate(-Vector3.up * rotation);

            }
            else if (Input.GetKey(KeyCode.E))
            {
                float rotation = rotatespeed * Time.deltaTime;
                xRotation += rotation;
                playerBody.Rotate(Vector3.up * rotation);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }

    private void FixedUpdate()
    {
        if (PV.IsMine && transform.parent.name == "Local")
        {
            // mvmt
            rb.velocity = movement * mvmtSpeed;
        }
    }

}

