using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnableSmoke
{
    public static bool inEnemyKitchen = false;
    public static GameObject smokeSlot;
    public static bool usedUp;

    // Gets state of player whether in enemy kitchen or not
    public bool GetPlayerState()
    {
        return inEnemyKitchen;
    }

    // Initialize smoke slot so can be used in other functions
    public void SetUIForSmoke(GameObject slot)
    {
        smokeSlot = slot;
        smokeSlot.SetActive(false);
    }

    // Disables UI for smoke slot
    public void DisableSmokeSlot()
    {
        smokeSlot.SetActive(false);
    }

    public void ChangePlayerState(bool playerState)
    {
        if (!usedUp)
        {
            inEnemyKitchen = playerState;
            if (smokeSlot != null && playerState)
            {
                smokeSlot.SetActive(true);
            }
            else if (smokeSlot && !playerState)
            {
                smokeSlot.SetActive(false);
            }
        }
    }

    public void UseSmoke()
    {
        usedUp = true;
    }

}

public class SmokeGrenade : MonoBehaviour
{
    private bool used = false;

    readonly EnableSmoke enableSmoke = new EnableSmoke();

    // Update is called once per frame
    void Update()
    {
        // if player in enemy kitchen
        if (enableSmoke.GetPlayerState()) {
            // and player presses 1, and has not been used before
            if (Input.GetKeyDown(KeyCode.Alpha1) && !used) {
                // lock player from using another smoke
                used = true;
                enableSmoke.UseSmoke();

                InstantiateSmokeBomb();
            } 
        }
    }

    void InstantiateSmokeBomb() {
        enableSmoke.DisableSmokeSlot();

        // syncs smoke bomb on network
        if (this.GetComponent<PhotonView>().IsMine)
            PhotonNetwork.Instantiate("smoke_grenade", transform.position + (transform.forward * 2), transform.rotation, 0);
    }
}
