using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public FriedFoodController friedFood;
    public List<FriedFoodController> stackedFood = new List<FriedFoodController>();
    public PanController pan;
    private Vector2 screenBounds;
    public float totalPoints;
    public float speed = 10f;

    void Start()
    {
        totalPoints = 0;
        friedFood = pan.friedFood;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if(pan.friedFood != null) {
            friedFood = pan.friedFood;
        
            if(friedFood.gameObject.transform.position.x < screenBounds.x || friedFood.gameObject.transform.position.y < screenBounds.y) {
                friedFood.timer.Reset();
                Destroy(friedFood.gameObject);
            }
        }

     //movement: only if it's player2, rpcs for player1

        if (Input.GetKey(KeyCode.LeftArrow)) {
            if(!(transform.position.x < screenBounds.x)) gameObject.transform.Translate(Vector3.left * speed * 10 * Time.deltaTime);
        }
		if (Input.GetKey(KeyCode.RightArrow)) {
            Vector2 panPos = pan.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
            Vector2 platePos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            if (platePos.x + 370 < panPos.x) gameObject.transform.Translate(Vector3.right * speed * 10 * Time.deltaTime);
        }
       
    }
 
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("plate hit");
        var obj = col.gameObject.GetComponent<FriedFoodController>();
        totalPoints += obj.points;
        GameEvents.current.assignPointsEventFunction();
        Debug.Log("total points:" + totalPoints);
        //obj.gameObject.transform.SetParent(this.gameObject.transform);
        Vector2 platePos = this.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        obj.gameObject.GetComponent<RectTransform>().anchoredPosition = platePos;
       // stackedFood.Add(friedFood);
        Debug.Log("food destroyed");
        Destroy(obj.gameObject);
        //friedFood = null;
    }
}
