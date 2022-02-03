using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickUpController : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player;
    public float pickUpRange;
    
    private bool equipped;
    private bool slotFull;

    private Vector3 half = new Vector3(0.5f, 0.5f, 0.5f);
   
    
    // Update is called once per frame
    void Update()
    {
            if(!equipped && Input.GetKeyDown(KeyCode.X) && !slotFull) {
                if(player != null){
                    GameObject p = GameObject.FindWithTag("Player");
                    player = p.GetComponent<Transform>();
                 }
                
                Vector3 distanceToPlayer = player.position - transform.position;
                if(distanceToPlayer.magnitude <= pickUpRange){
                    PickUp();
                }
                Debug.Log(player.position);
            }
            else if(equipped && Input.GetKeyDown(KeyCode.X)) {
                Drop();
            }
    }
    private void PickUp() {

        equipped = true;
        slotFull = true;
        transform.parent = GameObject.Find("slot").transform;
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