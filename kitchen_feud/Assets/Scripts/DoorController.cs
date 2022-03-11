using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.openDoor += openDoorUp;
    }

    // Update is called once per frame
    private void openDoorUp() {
        Debug.Log("Door up!");
        LeanTween.moveLocalY(gameObject, 4.56f, 1f).setEaseOutQuad();
    }
}
