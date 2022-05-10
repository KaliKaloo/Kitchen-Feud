using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientMove : MonoBehaviour
{

    public float minXSpeed;
    public float maxXSpeed;
    public float minYSpeed;
    public float maxYSpeed;
   
    public float destroyTime;

    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
        Destroy(this.gameObject, this.destroyTime);
    }
}