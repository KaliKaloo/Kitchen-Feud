using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DoorController : MonoBehaviour
{
    public int id;
    public float pos;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.openDoor += openDoorUp;
        GameEvents.current.closeDoor += openDoorDown;
        pos = this.transform.localPosition.y;
        //photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    private void openDoorUp(int id) {
        if(id == this.id) {
            Debug.Log("Door up!");
            LeanTween.moveLocalY(gameObject, /*this.transform.localPosition.y +*/pos + 6f, 1f).setEaseOutQuad();
            GetComponent<PhotonView>().RPC("RPCDoorUp", RpcTarget.Others);
        }
    }
    private void openDoorDown(int id) {
        if(id == this.id) {
            Debug.Log("Door down!");
            LeanTween.moveLocalY(gameObject, pos/*this.transform.localPosition.y - 6f*/, 1f).setEaseOutQuad();
            GetComponent<PhotonView>().RPC("RPCDoorDown", RpcTarget.Others);
        }
    }

    /*[PunRPC]
     void onApplianceF()
    {
        this.onAppliance = false;
    }*/
    [PunRPC]
    private void RPCDoorUp()
    {
        LeanTween.moveLocalY(gameObject, /*this.transform.localPosition.y +*/pos + 6f, 1f).setEaseOutQuad();
    }
    [PunRPC]
    private void RPCDoorDown()
    { 
        LeanTween.moveLocalY(gameObject, pos/*this.transform.localPosition.y - 6f*/, 1f).setEaseOutQuad();
    }
}




