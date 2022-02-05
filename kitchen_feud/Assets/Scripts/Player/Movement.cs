using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public Rigidbody player;
    public float m_speed, rotatespeed;
    public static GameObject LocalPlayerInstance;
    [SerializeField] private Camera m_camera;
    PhotonView view;

    private void Start()
    {
        //m_camera = Camera.main;
        view = GetComponent<PhotonView>();
        player = GetComponent<Rigidbody>();
        if (!view.IsMine)
        {
            m_camera.enabled = false;
        }

        DontDestroyOnLoad(gameObject);

    }

    /*void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                player.velocity = transform.forward * m_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                player.velocity = -transform.forward * m_speed * Time.deltaTime;
            }
        }
    */
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                player.velocity = transform.forward * m_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                player.velocity = -transform.forward * m_speed * Time.deltaTime;
            }
        }
    }


}




   /* void Update()
     
    {
        if (view.IsMine)
        {


            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * m_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += -transform.forward * m_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
            }
        }
    }
}
   */
