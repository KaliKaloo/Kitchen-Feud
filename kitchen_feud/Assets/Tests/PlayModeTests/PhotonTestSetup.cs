using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.TestTools;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;


public class PhotonTestSetup
{
    PhotonTestLobby lobby = null;
   
    [OneTimeSetUp]
    public void SetUp()
    {
        GameObject obj = new GameObject();
        lobby = obj.AddComponent<PhotonTestLobby>();
        lobby.Connect();

    }

    [UnitySetUp]
    public IEnumerator UnitySetUp()
    {
        yield return new WaitWhile(() => !lobby.ready);
       //yield return null;
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        if (PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.LeaveRoom();
        }
        PhotonNetwork.Disconnect();
        GameObject fireExtObj = GameObject.Find("fireExtinguisher");;
        if (fireExtObj != null)
            PhotonNetwork.Destroy(fireExtObj);
    }

    //public GameObject CreatePhotonGameObject()
    //{
    //    GameObject obj = new GameObject();
    //    PhotonView pv = obj.AddComponent<PhotonView>();
    //    PhotonNetwork.AllocateViewID(pv);
    //    return obj;
    //}


    public class PhotonTestLobby : MonoBehaviourPunCallbacks
    {

        public bool ready = false;

        public void Connect()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = true;
            }
            else
            {
                PhotonNetwork.Disconnect();

                StartCoroutine(offlineMode());

            }
            
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
            
            SceneManager.LoadScene("kitchens Test");
            ready = true;
        }
        public IEnumerator offlineMode()
        {
            yield return new WaitForSeconds(2);
            PhotonNetwork.OfflineMode = true;
        }
    }
    
}
