using System.Collections;
using System.Collections.Generic;
using System;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

// IMPORTANT:
// timer and score parser class have been moved to separate scripts
// CHECK scripts/menu folder for the relevant scripts

public class scoreController : MonoBehaviour
{
    [SerializeField] private Text score1Text;
    [SerializeField] private Text score2Text;

    [SerializeField] private Text timerText;
    [SerializeField] private GameObject loadingScreen;
    public List<GameObject> trays = new List<GameObject>();
    float elapsed = 0f;

    // updates end scores to compare in game over scene
    private static ParseScore scores = new ParseScore();

    // global timer
    private static GlobalTimer timer = new GlobalTimer();

    private MusicManager music;
    private bool startGame = false;
    private ExitGames.Client.Photon.Hashtable lobby = new ExitGames.Client.Photon.Hashtable();
    public PhotonView PV;
    private CleanupRoom cleanupRoom;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        loadingScreen.SetActive(true);

      
        // send message to server that finished loading
        int currentPlayers = (int)PhotonNetwork.CurrentRoom.CustomProperties["Players"];
        if (currentPlayers > 0)
            lobby["Players"] = currentPlayers - 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
        CustomProperties.PlayerResetStats.ResetAll();

        cleanupRoom = this.GetComponent<CleanupRoom>();

        // start scores at 0
        score1Text.text = ConvertScoreToString(scores.GetScore1());
        score2Text.text = ConvertScoreToString(scores.GetScore2());
    }



    // Converts an integer to a string with proper comma notation
    private string ConvertScoreToString(int score)
    {
        return String.Format("{0:n0}", score);
    }

    private string ConvertSecondToMinutes(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string str = time.ToString(@"mm\:ss");
        return str;
    }

    void Update()
    {
        // update scores every frame
        if (SceneManager.GetActiveScene().name != "kitchens Test")
        {
            
            if (startGame)
            {
                score1Text.text = ConvertScoreToString(scores.GetScore1());
                score2Text.text = ConvertScoreToString(scores.GetScore2());

                // increment every second
                elapsed += Time.deltaTime;
                if (elapsed >= 1f)
                {
                    elapsed = elapsed % 1f;
                    OutputTime();
                }
            }
            else if (GameObject.FindGameObjectsWithTag("Player").Length < PhotonNetwork.CurrentRoom.PlayerCount)
            {
                // show waiting for others players menu

            }
            /*else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Players"] > 0)
            {
                // show waiting for others players menu
            }*/
            else
            {
                loadingScreen.SetActive(false);
                startGame = true;
                // start timer if not started yet
                timer.InitializeTimer();
                timerText.text = ConvertSecondToMinutes(timer.GetTime());
                music = FindObjectOfType<MusicManager>();
            }
        }
        else
        {
            loadingScreen.SetActive(false);
            startGame = true;
            // start timer if not started yet
            timer.InitializeTimer();
            timer.StartTimer(this);
            timerText.text = ConvertSecondToMinutes(timer.GetTime());
            music = FindObjectOfType<MusicManager>();
        }
    }

    // OutputTime is called once per second
    void OutputTime()
    {

        if (timer.GetLocalTime() > 0)
        {
            // updates timer and text in timer
            timerText.text = ConvertSecondToMinutes(timer.GetLocalTime());
        }

        // SIGNAL FOR GAME OVER:
        else
        {
            // load game over screen and send final scores
            for (int i = 0; i < trays.Count; i++)
            {
                Tray ts = trays[i].GetComponent<Tray>();
                ts.tray.trayID = null;
                ts.tray.ServingTray.Clear();
                ts.tray.objectsOnTray.Clear();
            }
            PhotonNetwork.LoadLevel("gameOver");
            // calls this to clean objects which need resetting
            cleanupRoom.Clean();
                        
            startGame = false;


            // sends to server that game has finished
            lobby["Players"] = PhotonNetwork.CountOfPlayersInRooms;
            PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
        }

    }

    
}