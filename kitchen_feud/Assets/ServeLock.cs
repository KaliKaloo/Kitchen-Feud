using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeLock : MonoBehaviour
{
    // Start is called before the first frame update
    PickupLock pickupLock = new PickupLock();


    // when serve canvas is disabled allows player to pickup items again
    void OnDisable()
    {
        pickupLock.Unlock();
    }

    // when enabled player can no longer pickup items
    void OnEnable()
    {
        pickupLock.Lock();
    }
}
