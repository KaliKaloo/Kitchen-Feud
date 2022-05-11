using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Controls the movement of the stove using RigidBody
public class StoveMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D pot;

    public float speed;
    private float xBound;

    private float lowerBound;
    private float upperBound;
    public Camera UICamera;

    void Start()
    {
        // Place pot and relevant items scaled to resolution
        int chosenY = (int)(2f * UICamera.orthographicSize);
        xBound = (int)(chosenY  * UICamera.aspect)/2;
        pot = GetComponent<Rigidbody2D>();
        lowerBound = pot.position.x - xBound;
        upperBound = pot.position.x + xBound;
        pot.position = new Vector3(Screen.width / 2, Screen.height / 5.5f,0);
    }

    // Controlling the pot movement
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
            pot.velocity = Vector2.right * speed;
        else if (h < 0)
            pot.velocity = Vector2.left * speed;
        else
            pot.velocity = Vector2.zero;

        pot.position = new Vector2(Mathf.Clamp(pot.position.x, lowerBound, upperBound),
            pot.position.y);
    }
}