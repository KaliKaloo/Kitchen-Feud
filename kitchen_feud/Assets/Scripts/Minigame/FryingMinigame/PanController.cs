using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanController : MonoBehaviour
{
    public Transform pan;
    public float clampDistance = 50f;
    public Vector3 startLocation;
    public float mouseCursorSpeed;
    
    //the fried food needs to depend on the dish
    public FriedFoodController friedFood;
    public GameObject friedFoodPrefab;
    public int foodInstances = 5;
    public float speedLimit = 2000;
    public int speedQueueCapacity = 50;
    private Queue<float> speeds;
    private float avgSpeeds;
    private bool haveAvg;
    public int foodInstancesCounter;
    public FryingTimerBar timer;
    //public float totalPoints;
    private bool pointsAssigned = false;

    void Start () {
        //totalPoints = 0;
        startLocation = pan.position;
        haveAvg = false;
        speeds = new Queue<float>();
        foodInstancesCounter = 0;

        Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        var temp = Instantiate(friedFoodPrefab, panPos, friedFoodPrefab.transform.rotation);
        friedFood = temp.GetComponent<FriedFoodController>();
        friedFood.pan = this;
        friedFood.gameCanvas = this.gameObject.transform.parent.transform.parent.gameObject;
        friedFood.timer = timer;
        Debug.Log(temp.name);
    }

    void Update()
    {
        mouseCursorSpeed = abs(Input.GetAxis("Mouse X") / Time.deltaTime);
        if (speeds.Count == speedQueueCapacity) {
            
            while (speeds.Count > 0) {
                avgSpeeds += speeds.Dequeue();
            }
            avgSpeeds = avgSpeeds / speedQueueCapacity;
            haveAvg = true;
                
            speeds.Clear();
            //Debug.Log(avgSpeeds);
        }
        else {
            speeds.Enqueue(mouseCursorSpeed);
        }
        

        Vector3 lastLocation = pan.position;
  
        if(Input.GetAxis("Mouse X")<0 && (startLocation.x - lastLocation.x < clampDistance || startLocation.x < lastLocation.x)){
            pan.Translate(Vector3.left * mouseCursorSpeed*2 * Time.deltaTime);
        }
        if(Input.GetAxis("Mouse X")>0 && ( lastLocation.x - startLocation.x < clampDistance || startLocation.x > lastLocation.x)){
            pan.Translate(Vector3.right * mouseCursorSpeed*2 * Time.deltaTime);
            if (avgSpeeds > speedLimit && haveAvg == true && pointsAssigned == false) {
                pointsAssigned = true;
                friedFood.FlipPancake();
                //totalPoints += friedFood.points;
                //Debug.Log("pancake flipped, points:" + friedFood.points); 
                //Debug.Log("totalPoints:" + totalPoints);
            }
        }

        if(foodInstancesCounter < foodInstances && friedFood == null) {
                Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
                var temp = Instantiate(friedFoodPrefab, panPos, friedFoodPrefab.transform.rotation);
                friedFood = temp.GetComponent<FriedFoodController>();
                friedFood.pan = this;
                friedFood.gameCanvas = this.gameObject.transform.parent.transform.parent.gameObject;
                friedFood.timer = timer;
                foodInstancesCounter++;
                pointsAssigned = false;
                Debug.Log(foodInstancesCounter);
        }
    }
    public float abs(float x) {
       float result = x < 0 ? -x : x;
       return result;
    }

}
