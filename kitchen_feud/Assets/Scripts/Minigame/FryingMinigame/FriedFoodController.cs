using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedFoodController : MonoBehaviour
{
    public PanController pan;
    public Vector3 startLocation;
    public bool isFlipped = false;
    public FryingTimerBar timer;
    //public float preFlipPoints;
    //public float postFlipPoints;
    public float points;
    public float minXSpeed;
    public float maxXSpeed;
    public float minYSpeed;
    public float maxYSpeed;
    public GameObject gameCanvas;

    void Start()
    {
        //you need to get the pancontroller for every instance of the prefab!
        gameObject.transform.parent = pan.gameObject.transform.parent;
        //startLocation = pan.transform.position;
        gameObject.transform.localPosition = Vector3.zero;
        points = 0;
        
        //gameCanvas = pan.transform.parent.gameObject.GetComponent<ExitFryingMinigame>().minigameCanvas;
    }

    void Update()
    {
        //pan.mouseCursorSpeed;
        //this.transform.Translate(Vector3.down * mouseCursorSpeed*2 * Time.deltaTime);
    }

    public void FlipPancake() {
        //NO ISFLIPPED - FLIP MULTIPLE TIMES INSTEAD
        //NO POST OR PREFLIPPOINTS, ADD UP MULTIPLE TIMES
        if (isFlipped == false) {
            Debug.Log("Flip pancake!");
            isFlipped = true;
            //preFlipPoints = timer.Reset();
        //}
       // else if (isFlipped == true) {
        //    Debug.Log("Finish game!");
       //     postFlipPoints = timer.Reset();
            points = timer.Reset();
            //timer.stopTimer = true;
        //    points = preFlipPoints + postFlipPoints;
            if(points != 0) GameEvents.current.assignPointsEventFunction();
            this.YeetPancake();
        }

    }
    
    public void YeetPancake() {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;

        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
        this.gameObject.transform.SetParent(gameCanvas.transform);
        Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = panPos;
        
    }
}
