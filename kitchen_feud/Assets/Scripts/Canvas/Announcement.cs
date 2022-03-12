using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcement : MonoBehaviour
{

    private static GlobalTimer timer = new GlobalTimer();
    private int halfTime;
    public GameObject announcementUI;


    void Start()
    {
        halfTime = timer.GetTotalTime()/2;
        Debug.Log( timer.GetTotalTime());
        Debug.Log("hi");
    }

    void Update()
    {
        int currentTime = timer.GetTime();
        if (currentTime <= halfTime){
            announcementUI.SetActive(true);
        }
        
    }
}
