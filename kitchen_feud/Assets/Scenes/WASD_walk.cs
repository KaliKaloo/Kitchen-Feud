using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD_walk : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.5f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //  Time.deltaTime balances back out different speeds of computers
        if (Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.S)) {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    // use FixedUpdate for calculating physics (e.g adding forces, changing velocity)
}
