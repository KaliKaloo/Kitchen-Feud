using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [Header("Gameplay")]
    public GameObject cutPrefab;
    public float cutLifetime;

    private bool dragging;
    //coordinates where we start the cut
    private Vector2 swipeStart;

    void Update()
    {
        //input.GetMouseButtonDown returns true the momement mouse pressed down
        if (Input.GetMouseButtonDown(0))
        {
            this.dragging = true;
            //to get the unity coordinates we need to access the camera\
            swipeStart = Input.mousePosition;
            //swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        }
        else if (Input.GetMouseButtonUp(0) && this.dragging)
        {
            this.dragging = false;
            this.SpawnCut();
        }
    }

    private void SpawnCut()
    {
        //Vector2 swipeEnd = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 swipeEnd = Input.mousePosition;
        //instantiate and set position of line renderer
        GameObject cutInstance = Instantiate(this.cutPrefab, this.swipeStart, Quaternion.identity);
        cutInstance.GetComponent<LineRenderer>().SetPosition(0, this.swipeStart);
        cutInstance.GetComponent<LineRenderer>().SetPosition(1, swipeEnd);

        //change how collider of line works
        //the colliders position system is relative to where we start drawing the cut
        Vector2[] colliderPoints = new Vector2[2];
        //colliderPoints[0] = Vector2.zero;
        colliderPoints[0] = Vector2.zero;
        colliderPoints[1] = swipeEnd - swipeStart;
        cutInstance.GetComponent<EdgeCollider2D>().points = colliderPoints;

        Destroy(cutInstance, this.cutLifetime);
    }
}
