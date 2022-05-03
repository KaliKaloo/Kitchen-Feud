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

// IMPORTANT:
// timer and score parser class have been moved to separate scripts
// CHECK scripts/menu folder for the relevant scripts

public class scoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score1Text;
    [SerializeField] private TextMeshProUGUI score2Text;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject timesUpCanvas;

    public List<GameObject> trays = new List<GameObject>();
    float elapsed = 0f;
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
    private CleanupRoom cleanupRoom;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;    
        PlayerPrefs.SetInt("disconnected", 1);
        PV = GetComponent<PhotonView>();
        loadingScreen.SetActive(true);
        timesUpAnimator = timesUpCanvas.GetComponent<Animator>();

      
        // send message to server that finished loading
        if (PhotonNetwork.CurrentRoom.CustomProperties["Players"] != null)
        {
            int currentPlayers = (int) PhotonNetwork.CurrentRoom.CustomProperties["Players"];



            if (currentPlayers > 0)
                lobby["Players"] = currentPlayers - 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
        }

        PlayerPrefs.Save();

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
                OutputTime();

                score1Text.text = ConvertScoreToString(scores.GetScore1());
                score2Text.text = ConvertScoreToString(scores.GetScore2());
           
                    // increment every second
                    /*elapsed += Time.deltaTime;
                    if (elapsed >= 1f)
                    {
                        elapsed = elapsed % 1f;

                        OutputTime();
                  
                    }
                
                else
                {
                    if (PhotonNetwork.CurrentRoom.CustomProperties["time"] != null)
                    {
                        timerText.text = PhotonNetwork.CurrentRoom.CustomProperties["time"].ToString();
                    }
                }*/
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
            
                 timer.SetLocalTime();
                 timerText.text = ConvertSecondToMinutes(timer.GetLocalTime());
                 timer.StartTimer(this);

                


            }
        }
        else
        {
            loadingScreen.SetActive(false);
            startGame = true;
            // start timer if not started yet
            timer.InitializeTimer();
            //timerText.text = ConvertSecondToMinutes(timer.GetLocalTime());
            timer.StartTimer(this);

        }
    }

    // OutputTime is called once per second
    void OutputTime()
    {
        if (!gameEnd)
        {
            if (timer.GetLocalTime() > 0)
            {
                StartCoroutine(getLocalTime());
            }

            // SIGNAL FOR GAME OVER:
            else if (gameOver == false)
            {

                // load game over screen and send final scores
                for (int i = 0; i < trays.Count; i++)
                {
                    Tray ts = trays[i].GetComponent<Tray>();
                    ts.tray.trayID = null;
                    ts.tray.ServingTray.Clear();
                    ts.tray.objectsOnTray.Clear();
                }

                StartCoroutine(playTimesUpAnimation());

                // stop checking time after
                gameEnd = true;

            }
        }
    }

    // plays animation and exits game
    public IEnumerator playTimesUpAnimation()
    {
        timesUpCanvas.SetActive(true);
        // play animation
        timesUpAnimator.SetBool("StartGameOver", true);
        yield return new WaitForSeconds(3);


        // do game over
        if (PhotonNetwork.IsMasterClient)
            // this will auto sync with all clients
            PhotonNetwork.LoadLevel("gameOver");

        // calls this to clean objects which need resetting
        cleanupRoom.Clean();
        timesUpAnimator.SetBool("StartGameOver", false);
        timesUpCanvas.SetActive(false);

        startGame = false;

        // sends to server that game has finished
        lobby["Players"] = PhotonNetwork.CountOfPlayersInRooms;
        PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
        gameOver = true;
    }


    public IEnumerator getLocalTime()
    {
        yield return new WaitForSeconds(1);
        timerText.text = ConvertSecondToMinutes(timer.GetLocalTime());

    }

    
}