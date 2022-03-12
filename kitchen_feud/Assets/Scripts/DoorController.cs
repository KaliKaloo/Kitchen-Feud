using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.openDoor += openDoorUp;
        GameEvents.current.closeDoor += openDoorDown;
    }

    // Update is called once per frame
    private void openDoorUp(int id) {
        if(id == this.id) {
            Debug.Log("Door up!");
            LeanTween.moveLocalY(gameObject, this.transform.localPosition.y + 2.28f, 1f).setEaseOutQuad();
        }
    }
    private void openDoorDown(int id) {
        if(id == this.id) {
            Debug.Log("Door down!");
            LeanTween.moveLocalY(gameObject, this.transform.localPosition.y - 2.28f, 1f).setEaseOutQuad();
        }
    }
}
