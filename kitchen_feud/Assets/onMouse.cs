using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouse : MonoBehaviour
{
   /* private void DetectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        
        if (hit.collider != null)
        {
            Debug.Log("hit");
        }
        
    } */ 
    
    

    void MouseInput()
    {
        /*   if (hit.collider != null)
           {
               Debug.Log(hit.collider.gameObject.name);
           }
   */
        /*  if (Input.GetMouseButton(0))
          {

              Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              //Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);
              for (int i = 0; i < hits.Length; i++)
              {
                  Debug.Log(Input.mousePosition);
                  Debug.Log(hits.Length);
              }

          }*/
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(Input.mousePosition);
            Debug.Log(hits.Length);
        }
    }
}

