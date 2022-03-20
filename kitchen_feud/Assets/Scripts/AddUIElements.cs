using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUIElements : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject smokeSlot;

    EnableSmoke enableSmoke = new EnableSmoke();


    // Start is called before the first frame update
    void Start()
    {
        enableSmoke.SetUIForSmoke(smokeSlot);
    }
}
