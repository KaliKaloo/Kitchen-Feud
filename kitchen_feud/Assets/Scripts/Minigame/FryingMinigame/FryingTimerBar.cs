using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryingTimerBar : MonoBehaviour
{
    public Slider slider;
    public float gameTime;
    public bool stopTimer;
    public bool resetTimer;
    public float time;

    void Start()
    {
        stopTimer = false;
        slider.maxValue = gameTime;
        slider.value = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        int seconds = Mathf.FloorToInt(time * 60f);
        if(time >= gameTime) stopTimer = true;
        if(stopTimer == false) slider.value = time;
        
    }
    public float Reset() {
        float tempTime = time;
        time = 0;
        slider.value = 0;
        //stopTimer = false;
        return SetFriedLevel(tempTime);
        
    }

    public float SetFriedLevel(float value)
    {
        return (float)(100f - abs(50f- value*5))/5;
    }

    public float abs(float x) {
       float result = x < 0 ? -x : x;
       return result;
    }
}
