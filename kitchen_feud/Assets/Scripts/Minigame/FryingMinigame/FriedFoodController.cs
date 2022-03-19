using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedFoodController : MonoBehaviour
{
    public PanController pan;
    public Vector3 startLocation;
    public bool isFlipped = false;
    public FryingTimerBar timer;
    public float points;
    public float minXSpeed;
    public float maxXSpeed;
    public float minYSpeed;
    public float maxYSpeed;
    public GameObject gameCanvas;

    public Appliance appliance;

//use dishSO to get sprite and set it as the friedFood image
    public DishSO dishSO;

    void Start()
    {
        gameObject.transform.SetParent(pan.gameObject.transform.parent);
        //startLocation = pan.transform.position;
        gameObject.transform.localPosition = Vector3.zero;
        points = 0;

        

    }

    void Update()
    {

    }

    public void FlipPancake() {

        if (isFlipped == false) {
            isFlipped = true;
            Debug.Log("Flip pancake!");
            points = timer.Reset();
            if(points != 0) GameEvents.current.assignPointsEventFunction();
            this.YeetPancake();
        }

    }
    
    public void YeetPancake() {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
        this.gameObject.transform.SetParent(gameCanvas.transform);
        Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = panPos;
        
    }
}
