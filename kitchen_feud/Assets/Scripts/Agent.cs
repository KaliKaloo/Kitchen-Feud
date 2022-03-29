using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Oven;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        Oven = GameObject.Find("Kitchen 1").GetComponentInChildren<ovenMiniGame>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(GameObject.Find("Local").transform.position);
        if (Oven.GetComponentInChildren<Timer>())
        {
            if (Oven.GetComponentInChildren<Timer>().timerFake == 7)
            {
                agent.SetDestination(Oven.GetComponentInChildren<Timer>().transform.position);
                isMoving = true;

            }

            float dist = RemainingDistance(agent.path.corners);
            
          
            if (dist <0.3 && dist !=0)
            {
                Debug.LogError("s");
                Oven.GetComponentInChildren<Timer>().GetComponentInChildren<exitOven>().TaskOnClick();
                isMoving = false;
            }
        }
    }
    

    public float RemainingDistance(Vector3[] points)
    {
        if (points.Length < 2) return 0;
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);
        return distance;
    }
}

