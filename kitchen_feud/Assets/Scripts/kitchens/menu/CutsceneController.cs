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

    private VideoPlayer videoPlayer;
    private menuController menu;
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
    private bool skipButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = video.GetComponent<VideoPlayer>();
        menu = GetComponent<menuController>();
        skipButtonText.text = "Skip";
    }

    public void VoteSkipCutscene()
    {
        // check if button hasn't been pressed
        if (!skipButtonPressed)
        {
            skipButtonPressed = true;
            // increment amount of people who pressed button on network
            if (customProperties["Skip"] != null)
                customProperties["Skip"] = (int)customProperties["Skip"] + 1;
            else
                customProperties["Skip"] = 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        // check if skip property has been properly set by server
        if (propertiesThatChanged["Skip"] != null)
        {
            // skip cutscene if amount of skips = number of players
            if ((int)propertiesThatChanged["Skip"] >= PhotonNetwork.CurrentRoom.PlayerCount)
                GetComponent<PhotonView>().RPC("SkipCutscene", RpcTarget.All);

            // change the button text to how many players have skipped so far
            else
                skipButtonText.text = (int)propertiesThatChanged["Skip"] + "/" + (int)propertiesThatChanged["Skip"];
        }
    }

    // Stops the video for all players in lobby
    [PunRPC]
    void SkipCutscene()
    {
        videoPlayer.Stop();


        // start the game once all players voted
        //GetComponent<PhotonView>().RPC("loadSceneP", RpcTarget.All);
        //PhotonNetwork.LoadLevel(1);
    }
}
