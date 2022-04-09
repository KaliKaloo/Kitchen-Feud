using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnableSmoke
{
    private static bool inEnemyKitchen = false;
    public static GameObject smokeSlot;
    public static bool usedUp;

    public void RestartState()
    {
        inEnemyKitchen = false;
        usedUp = false;
    }
    

    // Gets state of player whether in enemy kitchen or not
    public bool GetPlayerState()
    {
        return inEnemyKitchen;
    }


    public void SetPlayerState(bool state)
    {
        inEnemyKitchen = state;
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
    private static bool used = false;

    readonly EnableSmoke enableSmoke = new EnableSmoke();

    private void Start()
    {
        enableSmoke.RestartState();
    }

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

    public void UseSmoke()
    {
        if (enableSmoke.GetPlayerState() && !used)
        {
                // lock player from using another smoke
                used = true;
                enableSmoke.UseSmoke();

                InstantiateSmokeBomb();
        }
    }

    void InstantiateSmokeBomb() {
        enableSmoke.DisableSmokeSlot();

        GameObject localPlayer = GameObject.Find("Local");

        // syncs smoke bomb on network
        if (localPlayer.GetComponent<PhotonView>().IsMine)
            PhotonNetwork.Instantiate("smoke_grenade", localPlayer.transform.position + (localPlayer.transform.forward * 2), localPlayer.transform.rotation, 0);
    }
}
