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
        GameObject obj = new GameObject();
        lobby = obj.AddComponent<PhotonTestLobbyOne>();
        SceneManager.LoadScene("mainMenu");

        // lobby.Connect();

    }

    [UnitySetUp]
    public IEnumerator UnitySetUp()
    {

        yield return null;
        //yield return new WaitWhile(() => !lobby.ready);
    }

    [OneTimeTearDown]
    public void PhotonTearDown()
    {
        //SceneManager.LoadScene("kitchens Test");
        // yield return new WaitForSeconds(5);
 
        SceneManager.LoadScene("kitchens Test");
        PhotonNetwork.Disconnect();


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
                "test",
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
