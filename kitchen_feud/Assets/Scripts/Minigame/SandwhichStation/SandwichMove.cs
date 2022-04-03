using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SandwichMove : MonoBehaviour, IPointerClickHandler
{
    public SandwichController SandwichController;
    public string LayerID;
    public float speed;
    public SpriteAtlas imgAtlas;

    private Vector3 locationA;
    private Vector3 locationB;
    private Vector3 nextLocation;

    [SerializeField] private Vector3 perfectPosition;
    [SerializeField] private Transform leftLocation;
    [SerializeField] private Transform platform;
    [SerializeField] private Transform rightLocation;

    public bool stopped = false;
    
    // Start is called before the first frame update
    
    void Start()
    {
        // perfectPosition = (Screen.width/2, Screen.height/2, 0);
        perfectPosition = platform.localPosition;
        
        locationA = leftLocation.localPosition;
        locationB = rightLocation.localPosition;
        nextLocation = locationB;
        
        SandwichController = gameObject.GetComponentInParent<SandwichController>();
        platform.GetComponent<Image>().sprite = imgAtlas.GetSprite(LayerID);

    }   

    

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((!stopped) && (SandwichController.checkStoppedID(LayerID))){
            stopped = true;
            SandwichController.moving = false;
            SandwichController.CountStopped++;
            StopMove();
        }

    }

    void Update() 
    {
        if (!stopped) Move();
        if ((stopped) && (SandwichController.moving)) {
            stopped = false;
        }
    }


    void StopMove()
    {
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