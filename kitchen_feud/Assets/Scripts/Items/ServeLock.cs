using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stops player from picking up dish whilst clicking button
public class ServeLock : MonoBehaviour
{
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
