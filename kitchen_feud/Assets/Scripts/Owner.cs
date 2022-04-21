using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.AI;

public class Owner : MonoBehaviour
{
    public int team;

    public GameObject oven;

    private NavMeshAgent agent;

    private bool collected;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (team == 1)
        {
            oven = GameObject.Find("Oven1");
        }
        else if(team == 2)
        {
            oven = GameObject.Find("Oven2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!collected  && oven.GetComponent<Appliance>().minigameCanvas)
        {
            collectFromOven();
            collected = true;
        }
        
        Debug.LogError(agent.remainingDistance);

        if (agent.remainingDistance < Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete &&
            Math.Abs(agent.transform.position.x - oven.transform.position.x) < 1 && Math.Abs(agent.transform.position.z - oven.transform.position.z) < 2)
        {
            Debug.LogError("Hello");
            oven.GetComponent<Appliance>().minigameCanvas.GetComponentInChildren<exitOven>().TaskOnClick();
        }
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
