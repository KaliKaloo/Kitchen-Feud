using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameEvents : MonoBehaviour
{
    //Singleton GameEvents
    public static GameEvents current;
    private void Awake() {
        current = this;
    }
    public event Action assignPoints;
    public void assignPointsEventFunction() {
        if(assignPoints != null) {
            assignPoints();
        }
    }
}