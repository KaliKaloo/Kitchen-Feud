using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider coll) {
        GameEvents.current.openDoorEventFunction(id);
    }
    private void OnTriggerExit(Collider coll) {
        GameEvents.current.closeDoorEventFunction(id);
    }
}
