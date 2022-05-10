using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

public class CutsceneController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject skipButton;
    [SerializeField] private Text skipButtonText;
    [SerializeField] private GameObject skipButtonInstructions;

    public int voteCount;
    private menuController menu;
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
    private bool skipButtonPressed = false;
    private PhotonView PV;

    [Header("VideoController")]
    public VideoPlayer videoPlayer;

    public string cutsceneURL;
    public string instructionURL;

    public GameObject videoCanvas;
    bool instructionPlayed =false;


    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.targetTexture.Release();
        videoPlayer.url = cutsceneURL;
        menu = GetComponent<menuController>();
        skipButtonText.text = "Skip";
        PV = GetComponent<PhotonView>();
        videoPlayer.loopPointReached += CheckOver;
    }

 
    void CheckOver(UnityEngine.Video.VideoPlayer videoPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(!instructionPlayed){
                PV.RPC("playInstructionVideo", RpcTarget.All, PV.ViewID);
            }
            else{
                menu.startGame();
            }
        }
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

    IEnumerator tutorialLoadingScreen (int viewID){
        CutsceneController cutsceneC = PhotonView.Find(viewID).GetComponent<CutsceneController>();
        instructionPlayed = true;

        cutsceneC.menu.loadingScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        cutsceneC.videoPlayer.url = instructionURL;
        cutsceneC.videoPlayer.Play();
        cutsceneC.menu.loadingScreen.SetActive(false);
    }


    [PunRPC]
    void increment(int viewID)
    {
        int vC;
        CutsceneController cutsceneC = PhotonView.Find(viewID).GetComponent<CutsceneController>();
        PhotonView.Find(viewID).GetComponent<CutsceneController>().voteCount++;
        vC = cutsceneC.voteCount;
        cutsceneC.skipButtonText.text = vC + "/" + PhotonNetwork.CurrentRoom.PlayerCount;

        if (vC == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            cutsceneC.skipButton.SetActive(false);
            if (PhotonNetwork.IsMasterClient)
            {
                cutsceneC.skipButtonInstructions.SetActive(true);
            }
            cutsceneC.videoPlayer.Stop();
            videoPlayer.targetTexture.Release();
            StartCoroutine(tutorialLoadingScreen(viewID));

            
        }
    }

    [PunRPC]
    void playVideo(int viewID)
    {
        CutsceneController cutsceneC =   PhotonView.Find(viewID).GetComponent<CutsceneController>();
        cutsceneC.skipButtonInstructions.SetActive(false);
        cutsceneC.videoCanvas.SetActive(true);
        cutsceneC.videoPlayer.Play();

    }

    [PunRPC]
    void playInstructionVideo(int viewID)
    {
        CutsceneController cutsceneC =   PhotonView.Find(viewID).GetComponent<CutsceneController>();
        cutsceneC.skipButton.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        {
            cutsceneC.skipButtonInstructions.SetActive(true);
        }
        videoPlayer.targetTexture.Release();
        StartCoroutine(tutorialLoadingScreen(viewID));

    }
}
