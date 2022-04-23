using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.AI;

public class Owner : MonoBehaviour
{
    public int team;

    public GameObject oven;

    private NavMeshAgent agent;

    private bool collected;

    private bool faceforward;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (team == 1)
        {
            oven = GameObject.Find("Oven1");
            agent.SetDestination(new Vector3(12.61f,0.2f,-4.8f));

        }
        else if(team == 2)
        {
            oven = GameObject.Find("Oven2");
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (team == 1)
        {
            if (!faceforward && agent.transform.position.x > 12 && agent.transform.position.z < -4 && agent.remainingDistance ==  0)
            {
                agent.transform.rotation = Quaternion.Euler(0, 0, 0);
                faceforward = true;
            }
        }
        
        /*if (!collected  && oven.GetComponent<Appliance>().minigameCanvas)
        {
            collectFromOven();
            collected = true;
        }
        

        if (agent.remainingDistance < Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete &&
            Math.Abs(agent.transform.position.x - oven.transform.position.x) < 1 && Math.Abs(agent.transform.position.z - oven.transform.position.z) < 2)
        {
            Debug.LogError("Hello");
            oven.GetComponent<Appliance>().minigameCanvas.GetComponentInChildren<exitOven>().TaskOnClick();
            agent.GetComponent<PlayerHolding>().pickUpItem(oven.GetComponent<Appliance>().cookedDish);
        }*/
    }

    void collectFromOven()
    {
        Vector3 ovenDestination;
        Vector3 ovenPosition;
  

        ovenPosition = oven.transform.position;
        ovenDestination = new Vector3(ovenPosition.x , ovenPosition.y,ovenPosition.z - 1.6f);

        if (!agent.hasPath)
        {
            agent.SetDestination(ovenDestination);
        }
        
        

}
    
}
