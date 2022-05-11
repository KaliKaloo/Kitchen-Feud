using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUIElements : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject smokeSlot;

    EnableSmoke enableSmoke = new EnableSmoke();


    void Start()
    {
        enableSmoke.SetUIForSmoke(smokeSlot);
    }
}
