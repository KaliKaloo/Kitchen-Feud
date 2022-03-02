using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttingkeypress : MonoBehaviour
{
    public int finalPoints;

   public void pressedKey(){
        finalPoints = 10;
        Debug.Log("pressed button");
        GameEvents.current.assignPointsEventFunction();
    }
}
