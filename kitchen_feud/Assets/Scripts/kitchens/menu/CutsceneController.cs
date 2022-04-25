using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

public class CutsceneController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject video;
    [SerializeField] private GameObject skipButton;
    [SerializeField] private Text skipButtonText;
    public int voteCount;
    private VideoPlayer videoPlayer;
    private menuController menu;
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
    private bool skipButtonPressed = false;
    private PhotonView PV;

    bool switchScene = false;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = video.GetComponent<VideoPlayer>();
        menu = GetComponent<menuController>();
        skipButtonText.text = "Skip";
        PV = GetComponent<PhotonView>();
        videoPlayer.loopPointReached += CheckOver;
    }

 
    void CheckOver(UnityEngine.Video.VideoPlayer videoPlayer)
    {
         menu.startGame();
    }
    

    public void VoteSkipCutscene()
    {
        // check if button hasn't been pressed
        if (!skipButtonPressed)
        {
            PV.RPC("increment",RpcTarget.All,PV.ViewID);
            skipButtonPressed = true;
            // increment amount of people who pressed button on network
            if (customProperties["Skip"] != null)
                customProperties["Skip"] = (int)customProperties["Skip"] + 1;
            else
                customProperties["Skip"] = 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
        }
    }


    [PunRPC]
    void increment(int viewID)
    {
        int vC;
        PhotonView.Find(viewID).GetComponent<CutsceneController>().voteCount++;
        vC = PhotonView.Find(viewID).GetComponent<CutsceneController>().voteCount;
        skipButtonText.text = vC + "/" + PhotonNetwork.CurrentRoom.PlayerCount;
        if (vC == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            menu.startGame();
        }
        
    }
    // Stops the video for all players in lobby
    [PunRPC]
    void SkipCutscene()
    {
        videoPlayer.Stop();
    }
}
