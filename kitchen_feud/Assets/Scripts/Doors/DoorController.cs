using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DoorController : MonoBehaviour
{

    [SerializeField] private Animator mydoor = null;
    public string Open;
    public string Close;

    public PhotonView PV;
    public int count;
    public bool isOpened;
    private void Start()
    {
        isOpened = false;
        PV = GetComponent<PhotonView>();   
    }

  
    void OnTriggerEnter(Collider cube)
    {
        if (PV.IsMine)
        {
            // whenever anything enters the trigger, open the door
            if (cube.gameObject.tag == "Player" && isOpened == false)
            {

                PV.RPC("syncAnim", RpcTarget.AllBuffered, mydoor.GetComponent<PhotonView>().ViewID, Open);
                PV.RPC("syncIsOpened", RpcTarget.AllBuffered, 1);
                PV.RPC("incrementDoor", RpcTarget.AllBuffered);
            }
            else if (cube.gameObject.tag == "Player" && isOpened == true)
            {
                PV.RPC("incrementDoor", RpcTarget.AllBuffered);

            }
        }
    }
    void OnTriggerExit(Collider cube)
    {
        if (PV.IsMine)
        {


            // whenever anything enters the trigger, open the door
            if (cube.gameObject.tag == "Player" && isOpened == true)
            {
                if (count > 1)
                {
                    PV.RPC("decrementDoor", RpcTarget.AllBuffered);
                }
                else
                {
                    PV.RPC("syncAnim", RpcTarget.AllBuffered, mydoor.GetComponent<PhotonView>().ViewID, Close);
                    PV.RPC("syncIsOpened", RpcTarget.AllBuffered, 0);
                    PV.RPC("decrementDoor", RpcTarget.AllBuffered);
                }


            }
        }
    }

 
     
    [PunRPC]
    void syncAnim(int viewiD,string x)
    {   
        PhotonView.Find(viewiD).GetComponent<Animator>().Play(x,0,0.0f);
        //SOUND -----------------------------------------------------------------
        PhotonView.Find(viewiD).GetComponent<AudioSource>().Play();
        //-----------------------------------------------------------------------
    }
    [PunRPC]
    void syncIsOpened(int x)
    {
        if (x == 1)
        {
            isOpened = true;
        }
        else
        {
            isOpened = false;   
        }
    }
    [PunRPC]
    void incrementDoor()
    {
        count += 1;
    }
    [PunRPC]
    void decrementDoor()
    {
        count -= 1;
    }

}