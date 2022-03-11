using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider coll) {
        GameEvents.current.openDoorEventFunction();
    }
}
