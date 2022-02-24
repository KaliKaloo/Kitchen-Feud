using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class menuController : MonoBehaviourPunCallbacks
{
   
    [SerializeField] private GameObject usernameMenu;
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private GameObject lobbyMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject changeTeam1;
    [SerializeField] private GameObject changeTeam2;

    [SerializeField] private GameObject startLobbyButton;
    [SerializeField] private GameObject settingsButton;

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

    private int team1 = 2;
    private int team2 = 1;
    private int currentTeam = 1;

    private static GlobalTimer timer = new GlobalTimer();

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            usernameMenu.SetActive(true);
        } 
        else
        {
            // NEED TO ADD CURRENT USERNAME HERE
            greetingMenu.text = "Welcome back " + PhotonNetwork.LocalPlayer.NickName + "!";
            usernameMenu.SetActive(false);
            connectPanel.SetActive(true);
        }
    }


    // gets list of players in the lobby in string
    private string GetPlayers1()
    {
        string players = "Team 1:" + System.Environment.NewLine;
        // CHANGE HERE SO ONLY GRABS PLAYERS IN TEAM 1
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            players += player.NickName + System.Environment.NewLine;
        }
        return players;
    }

    private string GetPlayers2()
    {
        string players = "Team 2:" + System.Environment.NewLine;
        // CHANGE HERE SO ONLY GRABS PLAYERS IN TEAM 2
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            players += player.NickName + System.Environment.NewLine;
        }
        return players;
    }

    // Initializes/reinitializes all lobby variables
    private void InitializeLobby(string name)
    {
        connectPanel.SetActive(false);
        settingsMenu.SetActive(false);
        lobbyMenu.SetActive(true);
        lobbyName.text = name;
        playerList.text = GetPlayers1();
        playerList2.text = GetPlayers2();

        // won't allow normal user to start/edit game settings
        if (!PhotonNetwork.IsMasterClient) {
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

        // updates all users' timers based on changed settings from master
        this.GetComponent<PhotonView>().RPC("UpdateTimer", RpcTarget.Others, timer.GetCurrentTime());
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }

    // increases timer by 1 minute and updates text
    public void IncreaseTimer()
    {
        timerError.text = "";
        int timerCheck = timer.AddSubtractTimerValue(60);
        if (timerCheck == 2)
            timerError.text = "Too long!";
        else
            currentTime.text = timer.GetCurrentTimeString();
    }

    // decreases timer by 1 minute and updates text
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
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected");
    }

    public void ChangeUsernameInput()
    {
        if (usernameInput.text.Length >= 1 && usernameInput.text.Length <= 9)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void SetUsername()
    {
        usernameMenu.SetActive(false);
        connectPanel.SetActive(true);
        PhotonNetwork.NickName = usernameInput.text;
        greetingMenu.text = "Welcome " + usernameInput.text + "!";
    }

    // Create room here
    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(createGameInput.text, new Photon.Realtime.RoomOptions() { MaxPlayers = 8}, null);
    }

    // Leave existing lobby
    public void LeaveGame()
    {
        lobbyError.text = "";
        PhotonNetwork.LeaveRoom(false);
    }

    // JOIN EXISTING LOBBY HERE
    public void JoinGame()
    {
      PhotonNetwork.JoinRoom(joinGameInput.text);
    }

    // Load level once game is started
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount <= 4)
            PhotonNetwork.LoadLevel(1);
        else
            // change this to load larger kitchen if > 4 players!!!!!
            PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinedRoom()
    {   
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }

    public override void OnLeftRoom()
    {
        lobbyMenu.SetActive(false);
        connectPanel.SetActive(true);
        usernameMenu.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        lobbyError.text = "Lobby does not exist!";
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }

    public void SwitchToTeam1() {
        // CHANGE USER TO TEAM 1 HERE
        currentTeam = 1;

        // JUST TEST DELETE AFTER
        team2 -= 1;
        team1 += 1;
        print(team1);
        print(team2);
        this.GetComponent<PhotonView>().RPC("UpdateLobby", RpcTarget.All);
    }

    public void SwitchToTeam2()
    {
        // CHANGE USER TO TEAM 2 HERE
        currentTeam = 2;

        // JUST TEST DELETE AFTER
        team1 -= 1;
        team2 += 1;
        print(team1);
        print(team2);
        this.GetComponent<PhotonView>().RPC("UpdateLobby", RpcTarget.All);
    }




    // 0 means both balanced, 1 means team 1 can be joined only, 2 means team 2 can be joined only
    private int CheckTeamBalance()
    {
        int team1Added = team1 + 2;
        int team2Added = team2 + 2;
        if (((team1Added % team2Added) >= 1) && (team1Added > team2Added))
            return 2;
        else if (((team2Added % team1Added) >= 1) && (team2Added > team1Added))
            return 1;
        else
            return 0;
    }
        
    // Update is called once per frame
    void Update()
    {
        if (lobbyMenu.activeSelf) {
            int balance = CheckTeamBalance();
            if (balance == 2)
            {
                changeTeam1.SetActive(false);
                changeTeam2.SetActive(true);
            } else if (balance == 1)
            {
                changeTeam1.SetActive(true);
                changeTeam2.SetActive(false);
            } else
            {
                changeTeam1.SetActive(false);
                changeTeam2.SetActive(false);
            }
        }
    }

    [PunRPC]
    void UpdateLobby()
    {
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }

    [PunRPC]
    void UpdateTimer(int newTime)
    {
        timer.ChangeTimerValue(newTime);
    }
}


