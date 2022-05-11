using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using System.Collections;
using UnityEngine.SceneManagement;
using agora_gaming_rtc;
using System.IO;
using UnityEngine.Video;
using Random = System.Random;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class menuController : MonoBehaviourPunCallbacks
{

    public static menuController Instance;

    [SerializeField] private GameObject usernameMenu;
    private bool setInternetSpeed;
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private GameObject lobbyMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject userSettingsMenu;
    [SerializeField] private GameObject findLobbyMenu;
    [SerializeField] private GameObject reconnectMenu;
    [SerializeField] private GameObject changeTeam1;
    [SerializeField] private GameObject changeTeam2;

    [SerializeField] private GameObject startLobbyButton;
    [SerializeField] private GameObject settingsButton;

    [SerializeField] private GameObject roomListItemPrefab;

    [SerializeField] private InputField usernameInput;
    [SerializeField] private InputField createGameInput;
    [SerializeField] private InputField joinGameInput;

    [SerializeField] private GameObject startButton;

    [SerializeField] private Text greetingMenu;
    [SerializeField] private Text lobbyName;
    [SerializeField] private Text lobbyNameSettings;
    [SerializeField] private Text playerList;
    [SerializeField] private Text playerList2;
    [SerializeField] private Text timerError;
    [SerializeField] private Text currentTime;

    [SerializeField] private Text lobbyError;
    IRtcEngine rtcEngine;
    public int x;
    Random rnd = new Random();
    private bool calledRejoin = false;
    private bool createLobby = false;
    private bool isDisconnected = false;

    [SerializeField] public Transform roomListContent;

    public GameObject loadingScreen;
    [SerializeField] private GameObject loadingBarCanvas;
    [SerializeField] private Slider loadingBar;
    public bool startedGame;
    bool increased;
    bool decreased;
    private static GlobalTimer timer = new GlobalTimer();
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
    private ExitGames.Client.Photon.Hashtable lobby = new ExitGames.Client.Photon.Hashtable();
    Hashtable scene = new Hashtable();
    Hashtable playAgain = new Hashtable();
    PhotonView PV;


    private void Awake()
    {
        Instance = this;
    }

    // Sets player's custom property to given team number
    private void SetTeam(int teamNumber)
    {
        customProperties["Team"] = teamNumber;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
    }

    // Checks the amount of players existing on a particular team
    private int GetAmountOfPlayers(int team)
    {
        int total = 0;
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            // make sure their team has been set yet, if not they are not loaded properly
            try
            {
                if ((int)player.CustomProperties["Team"] == team)
                {
                    total += 1;
                }
            }
            catch
            {
                // do nothing if no players exist in that team   
            }
        }
        return total;
    }

    public void Start()
    {
        PV = GetComponent<PhotonView>();
        setInternetSpeed = false;
        if (!PhotonNetwork.IsConnected)
        {
            // if their stored userID value is not null, then reconnect using that existing userID
            if (!CheckStringNullorEmpty(PlayerPrefs.GetString("userID")))
                PhotonNetwork.AuthValues = new AuthenticationValues(PlayerPrefs.GetString("userID"));

            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            loadingScreen.SetActive(true);

            // set team by default as 1
            SetTeam(1);

            // if player has previously played, display their previous username
            if (!CheckStringNullorEmpty(PlayerPrefs.GetString("username")))
            {
                greetingMenu.text = "Welcome back " + PlayerPrefs.GetString("username") + "!";
                PhotonNetwork.NickName = PlayerPrefs.GetString("username");
                usernameMenu.SetActive(false);
                connectPanel.SetActive(true);
            }
            // otherwise ask them to make a username
            else
            {
                usernameMenu.SetActive(true);
                startButton.SetActive(false);
            }
        }
        else
        {
            // set photon's nickname to one stored in settings
            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("username");
            greetingMenu.text = "Welcome back " + PhotonNetwork.LocalPlayer.NickName + "!";
            usernameMenu.SetActive(false);
            connectPanel.SetActive(true);
        }
    }

    // try rejoining using their last lobby name
    public void TryRejoin()
    {
        calledRejoin = true;
        PlayerPrefs.SetString("rejoined", "true");
        PhotonNetwork.RejoinRoom(PlayerPrefs.GetString("lastLobby"));
    }

    // goes back to the main menu
    public void BackToMainMenu()
    {
        findLobbyMenu.SetActive(false);
        connectPanel.SetActive(true);
        userSettingsMenu.SetActive(false);
    }

    // shows find lobby panel
    public void FindLobby()
    {
        connectPanel.SetActive(false);
        findLobbyMenu.SetActive(true);
    }

    // shows settings menu in lobby
    public void OpenUserSettings()
    {
        userSettingsMenu.SetActive(true);
    }

    // on pressing no on the reconnect menu, hides
    public void HideReconnectMenu()
    {
        PlayerPrefs.SetInt("disconnected", 0);
        isDisconnected = false;
        PlayerPrefs.SetString("lastLobby", null);
        reconnectMenu.SetActive(false);
    }


    // gets list of players in the lobby in string
    private string GetPlayers(int team)
    {
        string players = System.Environment.NewLine;
        // CHANGE HERE SO ONLY GRABS PLAYERS IN TEAM 1
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if ((int)player.CustomProperties["Team"] == team)
            {
                players += player.NickName + System.Environment.NewLine;
            }
        }
        return players;
    }

    // Initializes/reinitializes all lobby variables
    private void InitializeLobby(string name)
    {
        connectPanel.SetActive(false);
        settingsMenu.SetActive(false);
        findLobbyMenu.SetActive(false);
        lobbyMenu.SetActive(true);
        lobbyName.text = name;
        playerList.text = GetPlayers(1);
        playerList2.text = GetPlayers(2);

        // won't allow normal user to start/edit game settings
        if (!PhotonNetwork.IsMasterClient)
        {
            startLobbyButton.SetActive(false);
            settingsButton.SetActive(false);
        }
    }

    // Loads canvas for settings allowing master user to change game timer
    public void LoadSettingsMenu()
    {
        // double check to make sure somehow non-master user can't access settings menu
        if (PhotonNetwork.IsMasterClient)
        {
            lobbyMenu.SetActive(false);
            settingsMenu.SetActive(true);
            timerError.text = "";
            lobbyNameSettings.text = PhotonNetwork.CurrentRoom.ToString();
            currentTime.text = timer.GetCurrentTimeString();
        }
    }

    // after leaving settings menu loads lobby back up for master user
    public void LoadBackToLobbyMenu()
    {
        timerError.text = "";
        timer.SetServerTime();

        // updates all users' timers based on changed settings from master
        this.GetComponent<PhotonView>().RPC("UpdateTimer", RpcTarget.Others, timer.GetTime());
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }

    // increases timer by 1 minute and updates text
    // only to be used by master client!!!
    public void IncreaseTimer()
    {
        timerError.text = "";
        int timerCheck = timer.AddSubtractTimerValue(60);
        if (timerCheck == 2)
            timerError.text = "Too long!";
        else
            currentTime.text = timer.GetCurrentTimeString();
    }

    // decreases lobby timer by 1 minute and updates text
    // only to be used by master client!!!
    public void DecreaseTimer()
    {
        timerError.text = "";
        int timerCheck = timer.AddSubtractTimerValue(-60);
        if (timerCheck == 1)
            timerError.text = "Too short!";
        else
            currentTime.text = timer.GetCurrentTimeString();
    }

    public override void OnConnectedToMaster()
    {
        // Join Photon Master server
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // check if their last lobby exists by creating a room with same name
        if (!CheckStringNullorEmpty(PlayerPrefs.GetString("lastLobby")) && !createLobby)
        {
            createLobby = true;
            PhotonNetwork.CreateRoom(PlayerPrefs.GetString("lastLobby"), new Photon.Realtime.RoomOptions(), null);
        }
        else
        {
            loadingScreen.SetActive(false);
        }
    }

    // continually checks whether the username you're entering is valid
    public void ChangeUsernameInput()
    {
        // must be between 1 and 9 characters long, otherwise button will hide
        if (usernameInput.text.Length >= 1 && usernameInput.text.Length <= 9)
            startButton.SetActive(true);
        else
            startButton.SetActive(false);
    }

    // Shows the change username menu
    public void ChangeUsernameOnClick()
    {
        userSettingsMenu.SetActive(false);
        usernameMenu.SetActive(true);
        startButton.SetActive(false);
    }

    // Sets username using button
    public void SetUsername()
    {
        usernameMenu.SetActive(false);
        connectPanel.SetActive(true);
        PlayerPrefs.SetString("username", usernameInput.text);
        PhotonNetwork.NickName = usernameInput.text;

        // save player prefs to ensure works with WebGL
        PlayerPrefs.Save();
        greetingMenu.text = "Welcome " + usernameInput.text + "!";
    }

    // resets username to null
    public void ResetUsername()
    {
        PlayerPrefs.SetString("username", null);
        PhotonNetwork.NickName = null;
        greetingMenu.text = "Username has not been set!";
        BackToMainMenu();
    }

    // resets id to null, restart game for changes to take effect
    public void ResetUserID()
    {
        PlayerPrefs.SetString("userID", null);
        BackToMainMenu();

    }

    // Create room here
    public void CreateGame()
    {
        if (createGameInput.text == "")
        {
            lobbyError.text = "Please name a lobby";
        }
        else
        {
            loadingScreen.SetActive(true);
            PhotonNetwork.CreateRoom(createGameInput.text.ToUpper(), new Photon.Realtime.RoomOptions() { MaxPlayers = 8 }, null);
        }
    }

    // Leave existing lobby
    public void LeaveGame()
    {
        RemovePlayerFromLobby();
        PlayerPrefs.SetString("lastLobby", null);
        lobbyError.text = "";
        connectPanel.SetActive(false);
        loadingScreen.SetActive(true);
        PhotonNetwork.LeaveRoom();
        loadingScreen.SetActive(false);
    }

    // JOIN EXISTING LOBBY HERE
    public void JoinGame()
    {
        if (joinGameInput.text != "")
        {
            connectPanel.SetActive(false);
            loadingScreen.SetActive(true);
            PhotonNetwork.JoinRoom(joinGameInput.text.ToUpper());
        }
        else
        {
            lobbyError.text = "Cannot be empty";
        }
    }

    public void JoinGameWithInfo(RoomInfo info)
    {
        loadingScreen.SetActive(true);
        PhotonNetwork.JoinRoom(info.Name);
    }

    // Load level once game is started
    public void StartScene()
    {
        lobbyMenu.SetActive(false);
        // Game can no longer be played after scene has started
        PhotonNetwork.CurrentRoom.IsOpen = false;
        // how long player's data is saved after disconnect (60 seconds here)
        PhotonNetwork.CurrentRoom.PlayerTtl = 60000;
        // after start button is pressed players can no longer join
        PV.RPC("playVideo", RpcTarget.All, PV.ViewID);
    }

    public void startGame()
    {
        // Set the time to whatever was set using the settings
        timer.SetServerTime();

        // Load into next scene
        GetComponent<PhotonView>().RPC("loadSceneP", RpcTarget.All);
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinedRoom()
    {
        if (PlayerPrefs.GetInt("disconnected") == 1 && isDisconnected)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                PlayerPrefs.SetInt("disconnected", 0);
                StartCoroutine(LoadScene());
            }
            else
            {
                // to do what happens when master client disconnects
            }
        }
        // else if player not in disconnected state load normally
        else if (!createLobby)
        {
            // tells player what their last joined lobby was so can reconnect if available
            PlayerPrefs.SetString("lastLobby", PhotonNetwork.CurrentRoom.Name);
            PlayerPrefs.SetString("userID", PhotonNetwork.LocalPlayer.UserId);

            rtcEngine = VoiceChatManager.Instance.GetRtcEngine();
            // TODO:: Explain pls hamza
            if (PhotonNetwork.IsMasterClient)
            {
                x = rnd.Next(11, 101);
                lobby["Lobby"] = x;
                PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
            }
            else
            {
                AddPlayerToLobby();
                x = (int)PhotonNetwork.CurrentRoom.CustomProperties["Lobby"];
            }
            string band = (string)PhotonNetwork.LocalPlayer.CustomProperties["Band"];
            if (band == "A")
            {
                rtcEngine.SetAudioProfile(AUDIO_PROFILE_TYPE.AUDIO_PROFILE_MUSIC_HIGH_QUALITY_STEREO,
                    AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_MEETING);

            }
            else if (band == "B")
            {
                rtcEngine.SetAudioProfile(AUDIO_PROFILE_TYPE.AUDIO_PROFILE_MUSIC_HIGH_QUALITY,
                    AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_MEETING);

            }
            else if (band == "C")

            {
                rtcEngine.SetAudioProfile(AUDIO_PROFILE_TYPE.AUDIO_PROFILE_MUSIC_STANDARD,
                    AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_MEETING);

            }
            else if (band == "D")
            {
                rtcEngine.SetAudioProfile(AUDIO_PROFILE_TYPE.AUDIO_PROFILE_SPEECH_STANDARD,
                    AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_MEETING);

            }
            rtcEngine.JoinChannel(x.ToString() + "Lobby");

            loadingScreen.SetActive(false);


            // Auto balance team on join
            if (CheckTeamBalance() == 2 && PhotonNetwork.CurrentRoom.PlayerCount != 1)
            {
                SetTeam(2);
            }

            this.GetComponent<PhotonView>().RPC("UpdateLobby", RpcTarget.All, PhotonNetwork.CurrentRoom.ToString());
        }
        isDisconnected = false;
    }

    public override void OnLeftRoom()
    {
        // Show lobby canvas
        createLobby = false;
        lobbyMenu.SetActive(false);
        loadingScreen.SetActive(false);
        connectPanel.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        reconnectMenu.SetActive(false);
        loadingScreen.SetActive(false);

        if (calledRejoin)
        {
            // if can't join room after trying to rejoin, stop
            lobbyError.text = "Lobby no longer exists!";
            PlayerPrefs.SetString("lastLobby", null);
            PlayerPrefs.SetInt("disconnected", 0);
            isDisconnected = false;
        }
        else
        {
            lobbyError.text = "Lobby does not exist!";
        }

        calledRejoin = false;
        connectPanel.SetActive(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // Load back to lobby menu
        loadingScreen.SetActive(false);
        isDisconnected = true;
        createLobby = false;
        reconnectMenu.SetActive(true);
        connectPanel.SetActive(true);
    }

    // Update lobby when a player enters the room
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }

    // Update lobby when a player leaves the room
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }

    public override void OnCreatedRoom()
    {
        // if they could create a lobby with the same name as their last lobby, then it no longer exists
        if (createLobby)
        {
            PlayerPrefs.SetString("lastLobby", null);
            PhotonNetwork.LeaveRoom();
        }
        // Create normal room otherwise
        else
        {
            PlayerPrefs.SetString("lastLobby", PhotonNetwork.CurrentRoom.Name);
            PlayerPrefs.SetString("userID", PhotonNetwork.LocalPlayer.UserId);
            PlayerPrefs.SetInt("disconnected", 0);
            loadingScreen.SetActive(false);
            lobby["Players"] = 1;
            lobby["Skip"] = 0;
            timer.SetServerTime();
            PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
        }
    }

    // Adds a player count to the custom properties so that can be used to check who's loaded in
    private void AddPlayerToLobby()
    {
        int currentPlayers = (int)PhotonNetwork.CurrentRoom.CustomProperties["Players"];
        lobby["Players"] = currentPlayers + 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
    }

    // Removes a player count from the custom properties
    private void RemovePlayerFromLobby()
    {
        int currentPlayers = (int)PhotonNetwork.CurrentRoom.CustomProperties["Players"];
        if (currentPlayers > 1)
            lobby["Players"] = currentPlayers - 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(lobby);
    }

    // When pressing on button to switch team, switches and tells everyone's lobby to update
    // Set the teamNumber in inspector inside Unity!!!
    public void SwitchToTeam(int teamNumber)
    {
        SetTeam(teamNumber);
        this.GetComponent<PhotonView>().RPC("UpdateLobby", RpcTarget.All, PhotonNetwork.CurrentRoom.ToString());
    }

    // 0 means both balanced, 1 means team 1 can be joined only, 2 means team 2 can be joined only
    private int CheckTeamBalance()
    {
        int team1Amount = GetAmountOfPlayers(1);
        int team2Amount = GetAmountOfPlayers(2);
        // if part of team 1 and space in team 2 return 2
        if (team1Amount > team2Amount && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
            return 2;
        else if (team2Amount > team1Amount && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
            return 1;
        else
            return 0;
    }

    // based on balance of teams, will hide or unhide buttons to allow/disallow joining the other team
    private void UpdateTeamButtons()
    {
        int balance = CheckTeamBalance();
        if (balance == 2)
        {
            changeTeam1.SetActive(false);
            changeTeam2.SetActive(true);
        }
        else if (balance == 1)
        {
            changeTeam1.SetActive(true);
            changeTeam2.SetActive(false);
        }
        else
        {
            changeTeam1.SetActive(false);
            changeTeam2.SetActive(false);
        }
    }

    // Updates the lobby parts which need to be updated every frame
    void UpdateLobby()
    {
        if (lobbyMenu.activeSelf)
        {
            playerList.text = GetPlayers(1);
            playerList2.text = GetPlayers(2);
            UpdateTeamButtons();
        }

    }

    // Load next scene asynchronously (showing loading screen whilst actually loading)
    IEnumerator LoadScene()
    {
        lobbyMenu.SetActive(false);
        loadingScreen.SetActive(true);
        loadingBarCanvas.SetActive(true);
        while (PhotonNetwork.LevelLoadingProgress < 1.0f)
        {
            loadingBar.value = PhotonNetwork.LevelLoadingProgress;
            yield return null;
        }
        loadingBarCanvas.SetActive(false);

    }

    // returns true if string is null or empty
    private bool CheckStringNullorEmpty(string stringInput)
    {
        if (stringInput == null || stringInput == "")
            return true;
        return false;
    }


    // Update is called once per frame
    void Update()
    {

        if (PhotonNetwork.IsMasterClient && !increased)
        {
            IncreaseTimer();
            increased = true;
        }
        if (PhotonNetwork.IsMasterClient && !decreased)
        {
            DecreaseTimer();
            decreased = true;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (!startLobbyButton.activeSelf)
            {
                startLobbyButton.SetActive(true);
            }
        }
        UpdateLobby();

    }

    [PunRPC]
    void UpdateLobby(string roomName)
    {
        PlayerPrefs.SetInt("disconnected", 0);
        InitializeLobby(roomName);
    }

    [PunRPC]
    void UpdateTimer(int newTime)
    {
        timer.ChangeTimerValue(newTime);
    }
    [PunRPC]
    void loadSceneP()
    {
        StartCoroutine(LoadScene());
    }
}
