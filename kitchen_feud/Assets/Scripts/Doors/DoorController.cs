using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Smoothly open a door
   /* public AnimationCurve openSpeedCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) }); //Contols the open speed at a specific time (ex. the door opens fast at the start then slows down at the end)
    public float openSpeedMultiplier = 2.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = 90.0f; //Global door open speed that will multiply the openSpeedCurve

    bool open = false;
    bool enter = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;

    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;

        //Set Collider as trigger
        GetComponent<Collider>().isTrigger = true;
    }

    // Main function
    void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(openTime);
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);

        if (enter)
        {
            open = !open;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }
    }

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider other)
    {
        enter = true;
        Debug.Log("enter");
    }

    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider other)
    {
        enter = false;
        Debug.Log("Exit");
    }*/


    /*public GameObject Door;
    // the door copied and rotated/moved to be the opened door
    public GameObject DoorOpen;
    // this will be a copy of the original door so that we have some numbers to work with.
    private GameObject DoorClosed;
    // this controls if the door is opened or closed.
    public bool isOpened = false;
   
    // this is the movement rate (if movemnt is applied to the door)
    public float moveSpeed = 3;
    // this is the rotation rate (if rotation is applied to the door)
    public float rotationSpeed = 90;
 
    void Start() {
        // copy the door to keep its position
        DoorClosed = Instantiate(Door, Door.transform.position, door.transform.rotation);
        // hide both the open and closed door
        DoorClosed.SetActive(false);
        DoorOpen.SetActive(false);
    }
   
    void Update(){
        // every frame, move the door towards the Open/Closed door
        var target = isOpened ? DoorOpen : DoorClosed;
        // these actually do the moving/rotating
        Door.position = Vector3.MoveTowards(Door.position, target.position, moveSpeed * Time.deltaTime);
        Door.rotation = Quaternion.RotateTowards(Door.rotation, target.rotation, rotateSpeed * Time.deltaTime);
    }
 
    void OnTriggerEnter(Collider cube)
    {
        // whenever anything enters the trigger, open the door
        isOpened = true;
    }
   
    void OnTriggerExit(Collider cube){
        // whenever anything exits the trigger, close the door.
        isOpened = false;
    }*/
    public bool isOpened;
    public float moveSpeed = 20f;
    public Vector3 pos;
    public Vector3 offset;
    void Start() {
        pos = this.gameObject.transform.position;
        Debug.Log(pos);
        offset = new Vector3(2.5f, 0f, 0f);
        isOpened = false;
    }
    void Update() {
        //var target = isOpened ? DoorOpen : DoorClosed;
        // these actually do the moving/rotating
        //if (isOpened) this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, pos + offset, moveSpeed * Time.deltaTime);
        //if (!isOpened) this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, pos, moveSpeed * Time.deltaTime);
        //this.rotation = Quaternion.RotateTowards(Door.rotation, target.rotation, rotateSpeed * Time.deltaTime);
        if(isOpened && this.gameObject.transform.position.x < pos.x + offset.x) {
            //this.gameObject.transform.Translate(moveSpeed * 5 * Time.deltaTime, 0f, 0f);
            gameObject.GetComponent<Animator>().Play("OpenDoor");
        }
        else if(!isOpened && this.gameObject.transform.position.x > pos.x) {
            //this.gameObject.transform.Translate(-moveSpeed * 5 * Time.deltaTime, 0f, 0f);
        }

    }
    void OnTriggerEnter(Collider cube)
    {
        // whenever anything enters the trigger, open the door
        if(cube.gameObject.tag == "Player") {
            isOpened = true;
            Debug.Log("player enter");
        }
    }
   
    void OnTriggerExit(Collider cube){
        // whenever anything exits the trigger, close the door.
        if(cube.gameObject.tag == "Player") {
            isOpened = false;
            Debug.Log("player exit");
        }
    }
}