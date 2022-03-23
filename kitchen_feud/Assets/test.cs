using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class test : MonoBehaviour,IPunObservable
{
    Transform Stove;
    // Start is called before the first frame update
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform Child = transform.GetChild(i);
                if(Child.name == "Island")
                {
                    Stove = Child.GetChild(0);
                }
                
            }
            if(Stove.childCount == 6)
            {
                Transform x = Stove.GetChild(5);
                for (int i = 0; i < x.childCount; i++) {
                    Debug.LogError(x.GetChild(i).name);
                    stream.SendNext(x.GetChild(i).position);
                }
                    
            }
        }
        else if (stream.IsReading)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform Child = transform.GetChild(i);
                if (Child.name == "Island")
                {
                    Stove = Child.GetChild(0);
                }

            }
            if (Stove.childCount == 6)
            {
                Transform x = Stove.GetChild(5);
                for (int i = 0; i < x.childCount; i++)
                {
                    Debug.LogError("received");
                    x.GetChild(i).position = (Vector3)stream.ReceiveNext();
                }

            }
        }
    }
}
