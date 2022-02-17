using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// basically global class which can be accessed from other scenes
// j = 0 means not initialized 
// j = 1 beginning has been initialized
// j = 2 game has ended and user wants to play again (i.e. skip username menu)
public class CheckEnd
{
    static int j = 0;

    public void Beginning()
    {
        j = 1;
    }

    public void End()
    {
        j = 2;
    }

    public bool IsBeginning()
    {
        if (j == 1) return true;
        else return false;
    }

    public bool IsEnd()
    {
        if (j == 2) return true;
        else return false;
    }

    public bool IsInitialized()
    {
        if ((j == 1) || (j == 2)) return false;
        else return true;
    }

    public string ReturnString()
    {
        return j.ToString();
    }
}

public class menuController : MonoBehaviourPunCallbacks
{
   
    [SerializeField] private GameObject usernameMenu;
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private GameObject lobbyMenu;

    [SerializeField] private InputField usernameInput;
    [SerializeField] private InputField createGameInput;
    [SerializeField] private InputField joinGameInput;

    [SerializeField] private GameObject startButton;

    [SerializeField] private Text greetingMenu;
    [SerializeField] private Text lobbyName;
    [SerializeField] private Text playerList;
    [SerializeField] private Text lobbyError;

    private static CheckEnd gameOver = new CheckEnd();

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (gameOver.IsInitialized())
        {
            gameOver.Beginning();
            PhotonNetwork.ConnectUsingSettings();
            usernameMenu.SetActive(true);
        } 
        else if (gameOver.IsEnd())
        {
            // NEED TO ADD CURRENT USERNAME HERE
            // greetingMenu.text = "Welcome " + usernameInput.text + "!";
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

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        InitializeLobby(PhotonNetwork.CurrentRoom.ToString());
    }


}


