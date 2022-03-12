using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcement : MonoBehaviour
{

    private static GlobalTimer timer = new GlobalTimer();
    private int halfTime;
    private int disappearTime;
    public GameObject announcementUI;


    void Start()
    {
        halfTime = timer.GetTotalTime()/2;
        disappearTime = halfTime - 7;
    }

    void Update()
    {
        int currentTime = timer.GetTime();
        if (currentTime == halfTime){
            announcementUI.SetActive(true);
        }else if (currentTime == disappearTime){
            announcementUI.SetActive(false);
        }
        
    }
}
