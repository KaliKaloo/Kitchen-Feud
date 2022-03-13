using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D pot;

    public float speed;
    private float xBound;

    private float lowerBound;
    private float upperBound;

    // Start is called before the first frame update
    void Start()
    {
        xBound = Screen.width / 2;
        pot = GetComponent<Rigidbody2D>();
        print(pot.position);
        lowerBound = pot.position.x - xBound;
        upperBound = pot.position.x + xBound;
        pot.transform.position = new Vector2(Screen.width / 2, Screen.height / 4.3f);

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
            pot.velocity = Vector2.right * speed;
        else if (h < 0)
            pot.velocity = Vector2.left * speed;
        else
            pot.velocity = Vector2.zero;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, lowerBound, upperBound),
            transform.position.y);
    }
}