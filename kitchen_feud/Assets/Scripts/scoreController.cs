using System.Collections;
using System.Collections.Generic;
using System;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;


// Handles the score and timer logic
// All scores are parsed to this script and displayed appropriately
// This also checks whether the game has ended by continually checking the timer
public class scoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score1Text;
    [SerializeField] private TextMeshProUGUI score2Text;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject timesUpCanvas;

    public List<GameObject> trays = new List<GameObject>();
    Hashtable ht = new Hashtable();

    // updates end scores to compare in game over scene
    private static ParseScore scores = new ParseScore();

    // global timer
    private static GlobalTimer timer = new GlobalTimer();
    private Animator timesUpAnimator;

    private bool startGame = false;
    private bool gameEnd = false;
    private bool gameOver;
    private ExitGames.Client.Photon.Hashtable lobby = new ExitGames.Client.Photon.Hashtable();
    public PhotonView PV;
    private bool counted;
    private CleanupRoom cleanupRoom;
    int count;

    void Start()
    {
        gameOver = false;    
        PlayerPrefs.SetInt("disconnected", 1);
        PV = GetComponent<PhotonView>();
        timesUpAnimator = timesUpCanvas.GetComponent<Animator>();
        cleanupRoom = this.GetComponent<CleanupRoom>();

        // send message to server that finished loading
        if (PhotonNetwork.CurrentRoom.CustomProperties["Players"] != null)
        {
            int currentPlayers = (int) PhotonNetwork.CurrentRoom.CustomProperties["Players"];

            if (currentPlayers > 0)
                lobby["Players"] = currentPlayers - 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
        }

        // Save internal values of webGL
        PlayerPrefs.Save();

        // Reset all stats
        CustomProperties.PlayerResetStats.ResetAll();

        // start scores at 0
        score1Text.text = ConvertScoreToString(scores.GetScore1());
        score2Text.text = ConvertScoreToString(scores.GetScore2());
    }



    // Converts an integer to a string with proper comma notation
    private string ConvertScoreToString(int score)
    {
        return String.Format("{0:n0}", score);
    }

    // Changes seconds into a more readable format of Minutes:Seconds
    private string ConvertSecondToMinutes(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string str = time.ToString(@"mm\:ss");
        return str;
    }

    void Update()
    {
        
        // Run game normally if not prompted into test world
        if (SceneManager.GetActiveScene().name != "kitchens Test")
        {
            // when the game has started
            if (startGame)
            {
                // continually update scores
                OutputTime();
                int score1 = scores.GetScore1();
                int score2 = scores.GetScore2();
                score1Text.text = ConvertScoreToString(score1);
                score2Text.text = ConvertScoreToString(score2);
                if (timer.GetLocalTime() > 5)
                {
                    reactScore(score1, score2);
                }
           
            }
            else
            {
                // once loaded in they get counted so the server knows they've loaded
                if (!counted)
                {
                    foreach (Photon.Realtime.Player p in PhotonNetwork.CurrentRoom.Players.Values)
                    {
                        if (p.CustomProperties["ViewID"] != null)
                        {

                            count += 1;
                        }
                    }
                    counted = true;
                }

                // once everyone in the game has "counted" then the game is ready
                if(count == PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    loadingScreen.SetActive(false);
                    startGame = true;

                    // start timer if not started yet
                    timer.SetLocalTime();
                    timerText.text = ConvertSecondToMinutes(timer.GetLocalTime());
                    timer.StartTimer(this);
                }

                // keep waiting whilst not all players loaded
                else
                {
                    count = 0;
                    counted = false;
                }

        
            }
        }

        // Perform test sequence
        else
        {
            loadingScreen.SetActive(false);
            startGame = true;
            // start timer if not started yet
            timer.InitializeTimer();
            timer.StartTimer(this);

        }
    }

    // react to big score difference by music pitching
    void reactScore(int score1, int score2){
        if (PhotonNetwork.LocalPlayer.CustomProperties["ViewID"] != null && ((int)PhotonNetwork.LocalPlayer.CustomProperties["ViewID"] != 0) && 
        PhotonView.Find((int)PhotonNetwork.LocalPlayer.CustomProperties["ViewID"]).gameObject != null) {
            GameObject localP = PhotonView.Find((int)PhotonNetwork.LocalPlayer.CustomProperties["ViewID"]).gameObject;
            int team = localP.GetComponent<PlayerController>().myTeam;
            if (!MusicManager.instance.priorityPitch && score1 != 0 && score2 != 0) // give pitching priority to other events
            {
                if ((score1 * 1.2 <= score2) || (score2 * 1.2 <= score1))
                {
                    if (team == 1)
                    {
                        float ratio = score2 / score1;
                        ratio = 1 - (1 - ratio) / 4;
                        float pitch = Mathf.Min(ratio, 1.3f);
                        pitch = Mathf.Max(pitch, 0.7f);
                        MusicManager.instance.musicReact(pitch);
                    }
                    else if (team == 2)
                    {
                        float ratio = score1 / score2;
                        ratio = 1 - (1 - ratio) / 4;
                        float pitch = Mathf.Min(ratio, 1.3f);
                        pitch = Mathf.Max(pitch, 0.7f);
                        MusicManager.instance.musicReact(pitch);
                    }
                }
                else
                {
                    MusicManager.instance.endReaction();
                }
            }
        }
    }


    // OutputTime checks if the game is over based on the timer
    void OutputTime()
    {
        if (!gameEnd)
        {
            if (timer.GetLocalTime() > 0)
            {
                // keep decrementing if not 0
                StartCoroutine(getLocalTime());
            }
            // SIGNAL FOR GAME OVER:
            else if (!gameOver)
            {
                // load game over screen and send final scores
                for (int i = 0; i < trays.Count; i++)
                {
                    Tray ts = trays[i].GetComponent<Tray>();
                    ts.tray.trayID = null;
                    ts.tray.ServingTray.Clear();
                    ts.tray.objectsOnTray.Clear();
                }

                StartCoroutine(PlayTimesUpAnimation());

                // stop checking time after
                gameEnd = true;

            }
        }
    }

    // plays animation and exits game
    public IEnumerator PlayTimesUpAnimation()
    {
        timesUpCanvas.SetActive(true);
        // play animation
        timesUpAnimator.SetBool("StartGameOver", true);

        // calls this to clean objects which need resetting
        cleanupRoom.Clean();

        // sends to server that game has finished
        lobby["Players"] = PhotonNetwork.CountOfPlayersInRooms;
        PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
        
        // Wait 5 secs for them to read Times up canvas
        yield return new WaitForSeconds(5);

        timesUpAnimator.SetBool("StartGameOver", false);
        timesUpCanvas.SetActive(false);

        startGame = false;
        gameOver = true;
        // do game over
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.DestroyAll();
            // this will auto sync with all clients
            PhotonNetwork.LoadLevel("gameOver");
        }


    }

    public IEnumerator getLocalTime()
    {
        yield return new WaitForSeconds(1);
        timerText.text = ConvertSecondToMinutes(timer.GetLocalTime());

    }

    
}