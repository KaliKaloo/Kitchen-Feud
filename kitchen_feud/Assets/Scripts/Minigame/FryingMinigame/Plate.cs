using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public FriedFoodController friedFood;
    public List<FriedFoodController> stackedFood = new List<FriedFoodController>();
    public PanController pan;

    // Start is called before the first frame update

    //the plate needs to:
    //add points on caught pancake
    //catch pancakes - when collision plate becomes the parent
    //stack pancakes: the caught foods are located a lil bit above one another
    //move and be controlled by another player
    void Start()
    {
        friedFood = pan.friedFood;
    }

    // Update is called once per frame
    void Update()
    {
        //friedFood = pan.GetComponent<FriedFoodController>();
        //TEMPORARY UNTIL THE COLLISIONS WORK
        if(pan.friedFood != null) friedFood = pan.friedFood;
        
        if(friedFood != null && friedFood.gameObject.transform.position.y < this.gameObject.transform.position.y) {
            //Destroy(friedFood.gameObject);
            
        }
    }
    //the collision doesn't seem to happen?
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("plate hit");
        //friedFood.gameObject.transform.SetParent(null);
        friedFood.gameObject.transform.SetParent(this.gameObject.transform);
        Vector2 platePos = this.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;
        friedFood.gameObject.GetComponent<RectTransform>().anchoredPosition = platePos;
        stackedFood.Add(friedFood);
        Debug.Log("food destroyed");
        Destroy(friedFood.gameObject);
        friedFood = null;
    }
}
