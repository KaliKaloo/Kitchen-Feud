using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CleanupRoom : MonoBehaviour
{
    // cleans up room when called
    // put all things you need resetting/destroyed here
    public void Clean()
    {
        CleanPlayerSlots();
    }
    
    // Destroy all slots player is holding
    private void CleanPlayerSlots() {
        GameObject localPlayer = GameObject.Find("Local");
        // Destroy all items player is holding when game ends
        Transform slots = localPlayer.transform.Find("slot");
        foreach (Transform child in slots) 
        {
            Destroy(child.gameObject);
        }
        localPlayer = null;
    }
}
