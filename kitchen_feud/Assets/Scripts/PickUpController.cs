using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, slot, fpsCam;

    public float pickUpRange;
    
    private bool equipped;
    private bool slotFull;
    private Vector3 half = new Vector3(0.5f, 0.5f, 0.5f);
    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.X) && !slotFull) {
            PickUp();
        }
        else if(equipped && Input.GetKeyDown(KeyCode.X)) {
            Drop();
        }
    }
    private void PickUp() {

        equipped = true;
        slotFull = true;
        transform.SetParent(slot);
        //transform.position = slot.position;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = half;

        rb.isKinematic = true;
        coll.isTrigger = true;
    }

    private void Drop() {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;
    }
}
