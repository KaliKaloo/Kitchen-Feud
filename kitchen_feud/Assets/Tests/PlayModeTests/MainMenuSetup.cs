using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MainMenuSetup
{
    PhotonTestLobbyOne lobby = null;

    [OneTimeSetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteAll();
        
        GameObject obj = new GameObject();
        lobby = obj.AddComponent<PhotonTestLobbyOne>();
        SceneManager.LoadScene("mainMenu");
    }


    [UnityTearDown]
    public IEnumerator PhotonTearDown()
    {
        SceneManager.LoadScene("Test");
        yield return new WaitForSeconds(4);
        PhotonNetwork.Disconnect();
        yield return new WaitForSeconds(4);

    }



    public class PhotonTestLobbyOne : MonoBehaviourPunCallbacks
    {

        public bool ready = false;

        public void Connect()
        {
            PhotonNetwork.OfflineMode = true;
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.CreateRoom(
                "test1",
                new RoomOptions { MaxPlayers = 1 }
            );
        }

        public override void OnCreatedRoom()
        {
            SceneManager.LoadScene("mainMenu");
            ready = true;
        }
        
    }
}

