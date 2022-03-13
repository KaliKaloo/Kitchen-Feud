using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class mouseControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed = false;
    public static mouseControl Instance;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }



    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
        if(GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 1) { 
        GameObject.FindGameObjectWithTag("Kick").GetComponent<PhotonView>().RPC("setPlayerPressing",RpcTarget.All,1,1);
        }
        else
        {
          GameObject.FindGameObjectWithTag("Kick").GetComponent<PhotonView>().RPC("setPlayerPressing",RpcTarget.All,1,2);
        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
          if(GameObject.Find("Local").GetComponent<PlayerController>().myTeam == 1) { 
        GameObject.FindGameObjectWithTag("Kick").GetComponent<PhotonView>().RPC("setPlayerPressing",RpcTarget.All,-1,1);
        }
        else
        {
          GameObject.FindGameObjectWithTag("Kick").GetComponent<PhotonView>().RPC("setPlayerPressing",RpcTarget.All,-1,2);
        }
    }

}
