using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : Interactable
{
    public TraySO tray;
    public BaseFood item;
    public GameObject objectHolding;
    public List<Transform> slots = new List<Transform>();

    PlayerHolding playerHold;
    public pickableItem pickable;
    // public GameObject obj;
    public override void Interact()
    {
        playerHold = player.GetComponent<PlayerHolding>();
        objectHolding = playerHold.heldObj;

        if (playerHold.items.Count == 1)
        {
            //add object holding to tray slot if tray slot empty
            if (tray.ServingTray.Count < 4)
            {
                foreach (Transform slot in slots)
                {
                    Debug.Log(slot.transform.GetChildCount());
                    if (slot.transform.GetChildCount() == 0)
                    {
                        if (objectHolding.GetComponent<Rigidbody>())
                        {
                            objectHolding.transform.parent = slot;
                            objectHolding.transform.localPosition = Vector3.zero;
                            objectHolding.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        }

                    }
                    break;
                }

                //add basefood item to list of foods of the tray
                pickable = objectHolding.GetComponent<pickableItem>();
                pickable.onTray = true;
                tray.ServingTray.Add(pickable.item);
                pickable.Tray = tray;
                Debug.Log(tray.ServingTray.Count);
                playerHold.dropItem();
            }

            else
            {
                //do nothing because there are no empty slots
                Debug.Log("this tray is full");

            }
        }


    }

   

}
 