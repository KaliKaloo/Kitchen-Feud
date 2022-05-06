using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class CleanupRoom : MonoBehaviour
{
    public Hashtable ht = new Hashtable();
    // cleans up room when called
    // put all things you need resetting/destroyed here
    public void Clean()
    {
        ht["loaded"] = 0;
        ht["Time"] = 0;
        ht["PlayingAgain"] = 1;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
        CleanPlayerSlots();


       
    }
    
    // Destroy all slots player is holding
    private void CleanPlayerSlots() {
        if (GameObject.Find("Local"))
        {
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
}
