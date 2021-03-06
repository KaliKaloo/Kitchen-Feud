
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;


public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;
    private int currentScene;
    public GameObject x;

    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;

    }

    public override void OnDisable()
    {
        base.OnEnable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;

    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == 1)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["loaded"] == null ||
                (int) PhotonNetwork.LocalPlayer.CustomProperties["loaded"] != 1)
            {
                CreatePlayer();
            }
        }
    }

    private void CreatePlayer()
    {
        x = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers", "Cube"), new Vector3(5, 1, -4), Quaternion.identity);
    }



}