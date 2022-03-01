using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttingkeypress : MonoBehaviour
{
    // Start is called before the first frame updat

    // Update is called once per frame
    public int finalPoints;

   public void pressedKey(){
        finalPoints = 10;
        Debug.Log("pressed x");
        GameEvents.current.assignPointsEventFunction();
    }
}
