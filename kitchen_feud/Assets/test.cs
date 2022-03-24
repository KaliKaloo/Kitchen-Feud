using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class test : MonoBehaviour,IPunObservable
{
    Transform Stove;
    Transform p;
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
                    if(x.GetChild(i).name == "PanGameObject")
                    {
                        p = x.GetChild(i);
                    }
                    
                }
                    
            }
            if (p)
            {
                stream.SendNext(p.position);
                stream.SendNext(p.GetComponent<RectTransform>().position);
                stream.SendNext(p.GetComponent<RectTransform>().anchoredPosition);
                Debug.LogError("Sent " + p.position + "RP " + p.GetComponent<RectTransform>().position +
                    " AP " );
                for (int i = 0; i < p.childCount; i++)
                {
                    if (p.GetChild(i).name != "pan")
                    {
                        //D
                        stream.SendNext(p.GetChild(i).position);
                    }
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
                    if (x.GetChild(i).name == "PanGameObject")
                    {
                        p = x.GetChild(i);
                    }

                }

            }
            if (p)
            {
                p.position = (Vector3)stream.ReceiveNext();
                p.GetComponent<RectTransform>().position = (Vector3)stream.ReceiveNext();
                p.GetComponent<RectTransform>().anchoredPosition = (Vector2)stream.ReceiveNext();
                Debug.LogError("receivedP " + p.position + "RP " + p.GetComponent<RectTransform>().position
                    + " AP " );

                for (int i = 0; i < p.childCount; i++)
                {
                    if (p.GetChild(i).name != "pan")
                    {
                        p.GetChild(i).position = (Vector3)stream.ReceiveNext();
                        //Debug.LogError("received " + p.GetChild(i).position);
                    }

                }

            }
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    Transform Child = transform.GetChild(i);
            //    if (Child.name == "Island")
            //    {
            //        Stove = Child.GetChild(0);
            //    }

            //}
            //if (Stove.childCount == 6)
            //{
            //    Transform x = Stove.GetChild(5);
            //    for (int i = 0; i < x.childCount; i++)
            //    {
            //        Debug.LogError("received");
            //        x.GetChild(i).position = (Vector3)stream.ReceiveNext();
            //    }

            //}
        }
    }
}
        
