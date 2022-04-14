using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TraySlotting : MonoBehaviour
{
    public List<Transform> slots = new List<Transform>();

    public PhotonView pV;
    // Start is called before the first frame update
    void Start()
    {
        pV = GetComponent<PhotonView>();
    }
    
    public void slotOnTray(GameObject dish)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].childCount == 0)
            {
                PhotonView dPv = dish.GetPhotonView();
                dPv.GetComponent<PhotonView>()
                    .RPC("setParent", RpcTarget.All, dPv.ViewID, slots[i].GetComponent<PhotonView>().ViewID);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
