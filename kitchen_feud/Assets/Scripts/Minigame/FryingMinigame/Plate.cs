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

    // Start is called before the first frame update

    //the plate needs to:
    //add points on caught pancake
    //catch pancakes - when collision plate becomes the parent
    //stack pancakes: the caught foods are located a lil bit above one another
    //move and be controlled by another player
    void Start()
    {
        totalPoints = 0;
        friedFood = pan.friedFood;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        //friedFood = pan.GetComponent<FriedFoodController>();
        //TEMPORARY UNTIL THE COLLISIONS WORK
        if(pan.friedFood != null) friedFood = pan.friedFood;
        
        /*if(friedFood != null && friedFood.gameObject.transform.position.y < this.gameObject.transform.position.y) {
            Destroy(friedFood.gameObject);
            
        }*/
        if(friedFood.gameObject.transform.position.x < screenBounds.x || friedFood.gameObject.transform.position.y < screenBounds.y) {
            friedFood.timer.Reset();
            Destroy(friedFood.gameObject);
        }
    }
 
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("plate hit");
        //friedFood.gameObject.transform.SetParent(null);
        var obj = col.gameObject.GetComponent<FriedFoodController>();
        totalPoints += obj.points;
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
