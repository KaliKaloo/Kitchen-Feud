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
    public event Action<int> openDoor;
    public void openDoorEventFunction(int id) {
        if(openDoor != null) {
            openDoor(id);
        }
    }
    public event Action<int> closeDoor;
    public void closeDoorEventFunction(int id) {
        if(closeDoor != null) {
            closeDoor(id);
        }
    }
}
