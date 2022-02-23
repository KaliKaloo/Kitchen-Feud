using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class menuController : MonoBehaviourPunCallbacks
{
   
    [SerializeField] private GameObject usernameMenu;
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private GameObject lobbyMenu;
    [SerializeField] private GameObject changeTeam1;
    [SerializeField] private GameObject changeTeam2;

    [SerializeField] private GameObject startLobbyButton;

    [SerializeField] private InputField usernameInput;
    [SerializeField] private InputField createGameInput;
    [SerializeField] private InputField joinGameInput;

    [SerializeField] private GameObject startButton;

    [SerializeField] private Text greetingMenu;
    [SerializeField] private Text lobbyName;
    [SerializeField] private Text playerList;
    [SerializeField] private Text playerList2;

    [SerializeField] private Text lobbyError;

    private int team1 = 2;
    private int team2 = 1;
    private int currentTeam = 1;

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
    private string GetPlayers()
    {
        string players = "Team 1:" + System.Environment.NewLine;
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            players += player.NickName + System.Environment.NewLine;
        }
        return players;
    }

    private void InitializeLobby(string name)
    {
        connectPanel.SetActive(false);
        lobbyMenu.SetActive(true);
        lobbyName.text = name;
        playerList.text = GetPlayers();
        playerList2.text = GetPlayers();
        if (!PhotonNetwork.IsMasterClient) {
            startLobbyButton.SetActive(false);
        }
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
        //PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(1);
        // PhotonNetwork.LoadLevel("kitchens (with score)");
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
}


