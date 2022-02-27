using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using agora_gaming_rtc;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class menuController : MonoBehaviourPunCallbacks
{
    IRtcEngine rtcEngine;
    [SerializeField] private GameObject usernameMenu;
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private GameObject lobbyMenu;

    [SerializeField] private InputField usernameInput;
    [SerializeField] private InputField createGameInput;
    [SerializeField] private InputField joinGameInput;
    public int myChannel = 0;

    [SerializeField] private GameObject startButton;

    [SerializeField] private Text greetingMenu;
    [SerializeField] private Text lobbyName;
    [SerializeField] private Text playerList;
    [SerializeField] private Text lobbyError;
    public static menuController Instance;
    public string appId = "906fd9f2074e4b0491fcde55c280b9e5";

    private void Awake()
    {
        if (menuController.Instance == null)
        {
            menuController.Instance = this;
        }
        else
        {
            if (menuController.Instance != this)
            {
                Destroy(menuController.Instance.gameObject);
                menuController.Instance = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()


    {


        rtcEngine = IRtcEngine.GetEngine(appId);
        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        rtcEngine.OnLeaveChannel += OnleaveChannel;
        rtcEngine.OnError += OnError;
        rtcEngine.EnableSoundPositionIndication(true);
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
        string players = "Players:" + System.Environment.NewLine;
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
        PhotonNetwork.CreateRoom(createGameInput.text, new Photon.Realtime.RoomOptions() { MaxPlayers = 8 }, null);
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



    void OnError(int error, string msg)
    {
        Debug.Log("Error with Agora: " + error + "This is the message: " + msg);
    }

    private void OnleaveChannel(RtcStats stats)
    {
        Debug.Log("Left channel with duration" + stats.duration);
    }

    private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.LogError("Joined channel: " + channelName);

        Hashtable hash = new Hashtable();
        hash.Add("agoraID", uid.ToString());
        PhotonNetwork.SetPlayerCustomProperties(hash);

    }
    public override void OnJoinedRoom()
    {
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
        rtcEngine.JoinChannel("Lobby");
       
        /*
        
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("HEYY");
            
            rtcEngine.JoinChannel("Team1");
            Debug.LogError(PhotonRoom.room.x.GetComponent<PhotonPlayer>().myTeam);
        }
        else
        {
            if (PhotonNetwork.CurrentRoom.Players.Count > 1 && PhotonNetwork.PlayerList.GetValue(1).Equals(PhotonNetwork.LocalPlayer))
            {
                //   Debug.Log(PhotonNetwork.CurrentRoom.Players[1].NickName);
                
                rtcEngine.JoinChannel("Team2");
                myChannel = 2;
            }else if(PhotonNetwork.CurrentRoom.Players.Count > 1 && PhotonNetwork.PlayerList.GetValue(2).Equals(PhotonNetwork.LocalPlayer))
            {
                myChannel = 1;
                rtcEngine.JoinChannel("Team1");
            }
        }
        */

        Debug.Log(PhotonNetwork.PlayerList[0].NickName);
    }

    public IRtcEngine GetRtcEngine()
    {
        return rtcEngine;
        
    }

    private void OnDestroy()
    {
        IRtcEngine.Destroy();
    }
    public override void OnLeftRoom()
    {
        lobbyMenu.SetActive(false);
        connectPanel.SetActive(true);
        usernameMenu.SetActive(false);
        //rtcEngine.LeaveChannel();
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
}