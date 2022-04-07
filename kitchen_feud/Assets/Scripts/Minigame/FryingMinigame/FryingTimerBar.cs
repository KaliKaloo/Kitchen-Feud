using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FryingTimerBar : MonoBehaviour
{
    public Slider slider;
    public float gameTime;
    public bool stopTimer;
    public bool resetTimer;
    public float time;
    public float points;
    public PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        stopTimer = false;
        slider.maxValue = gameTime;
        slider.value = 0;
    }

    void Update()
    {
        if (PV.IsMine)
        {
            time += Time.deltaTime;
            int seconds = Mathf.FloorToInt(time * 60f);
            if (time >= gameTime)
            {
                //stopTimer = true;
                time = 0;
                slider.value = 0;
            }

            PV.RPC("syncSlider", RpcTarget.All, PV.ViewID, time);
        }
        
    }
    public void Reset() {
        
        float tempTime = time;
        time = 0;
        if (slider)
            slider.value = 0;
        //stopTimer = false;
        points = SetFriedLevel(tempTime);
        //return SetFriedLevel(tempTime);
        
    }

    public float SetFriedLevel(float value)
    {
        return (float)(100f - Mathf.Abs(50f- value*5))/5;
    }

    [PunRPC]
    void syncSlider(int viewID,float time)
    {
        PhotonView.Find(viewID).GetComponent<FryingTimerBar>().slider.value = time;
    }
}