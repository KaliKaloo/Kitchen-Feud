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
        GameObject.FindGameObjectWithTag("Kick").GetComponent<PhotonView>().RPC("setPlayerPressing",RpcTarget.All,1);
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
        GameObject.FindGameObjectWithTag("Kick").GetComponent<PhotonView>().RPC("setPlayerPressing",RpcTarget.All,-1);
    }

}
