using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandwichMove : MonoBehaviour, IPointerClickHandler
{
    public SandwichController SandwichController;
    
    //private Vector3 perfectPosition;
    private Vector3 locationA;
    private Vector3 locationB;
    private Vector3 nextLocation;

    [SerializeField] private Vector3 perfectPosition;
    [SerializeField] private Transform leftLocation;
    [SerializeField] private Transform platform;
    [SerializeField] private Transform rightLocation;

    public bool stopped = false;
    public float speed;
    // Start is called before the first frame update
    
    void Start()
    {
        perfectPosition = platform.localPosition;
        locationA = leftLocation.localPosition;
        locationB = rightLocation.localPosition;
        nextLocation = locationB;
        
        SandwichController = gameObject.GetComponentInParent<SandwichController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!stopped){
        stopped = true;
        SandwichController.CountStopped++;
        StopMove();
        }
    }

    //void StartMoving
    void Update()
    {
        //while
        if (!stopped) Move();
        
    }

    void StopMove()
    {
        Debug.Log("ive been stopped");
        Vector3 stoppedPosition = platform.localPosition;
        float distance = Vector3.Distance(platform.localPosition, perfectPosition);
              
        CalculateScore(distance);

    }

    void CalculateScore(float score)
    {
        int finalScore = 25 - (int)((score/600) * 25); 
        SandwichController.Score += finalScore;
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