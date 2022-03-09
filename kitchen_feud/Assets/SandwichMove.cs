using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SandwichMove : MonoBehaviour
{
    private Vector3 locationA;
    private Vector3 locationB;
    private Vector3 nextLocation;

    [SerializeField] private Transform leftLocation;
    [SerializeField] private Transform platform;
    [SerializeField] private Transform rightLocation;

    public bool stopped = false;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        locationA = leftLocation.localPosition;
        locationB = rightLocation.localPosition;
        nextLocation = locationB;
    }

   

    void Update()
    {
        if (!stopped) Move();
        else if (stopped) StopMove();
    }

    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        Destroy(gameObject);
    }

    void StopMove()
    {
        Debug.Log("ive been stopped");
    }

    private void Move()
    {
        platform.localPosition = Vector3.MoveTowards(platform.localPosition, nextLocation, speed * Time.deltaTime);

        if (Vector3.Distance(platform.localPosition, nextLocation) <= 0.1)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        nextLocation = nextLocation != locationA ? locationA : locationB;
    }
}