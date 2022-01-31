using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{ 
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject usernameMenu;
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private GameObject lobbyMenu;

    [SerializeField] private InputField usernameInput;
    [SerializeField] private InputField createGameInput;
    [SerializeField] private InputField joinGameInput;

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject leaveButton;
    [SerializeField] private GameObject startGameButton;

    [SerializeField] private Text greetingMenu;
    [SerializeField] private Text lobbyName;
    [SerializeField] private Text playerList;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    private void Start()
    {
        greetingMenu.text = "";
        usernameMenu.SetActive(true);
        lobbyMenu.SetActive(false);
    }

    private string GetPlayers()
    {
        string players = "Players:" + System.Environment.NewLine;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            players += player.NickName + System.Environment.NewLine;
        }
        return players;
    }

    private void InitializeLobby(string name)
    {
        connectPanel.SetActive(false);
        lobbyMenu.SetActive(true);
        lobbyName.text = "Lobby: " + name;
        playerList.text = GetPlayers();
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChangeUsernameInput()
    {
        if (usernameInput.text.Length >= 1)
        {
            startButton.SetActive(true);
        } else
        {
            startButton.SetActive(false);
        }
    }

    public void SetUsername()
    {
        usernameMenu.SetActive(false);
        PhotonNetwork.playerName = usernameInput.text;
        greetingMenu.text = "Welcome " + usernameInput.text + "!"; 
    }

    public void CreateGame()
    {
        // CREATE LOBBY HERE
        //PhotonNetwork.CreateRoom(createGameInput.text, new RoomOptions() { MaxPlayers = 8 }, null);

        InitializeLobby(createGameInput.text);
    }

    public void LeaveGame()
    {
        lobbyMenu.SetActive(false);
        connectPanel.SetActive(true);

        // LEAVE ROOM HERE
        //PhotonNetwork.LeaveRoom();
    }

    public void JoinGame()
    {   
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;

        // JOIN EXISTING LOBBY HERE
        // PhotonNetwork.JoinOrCreateRoom(joinGameInput.text, roomOptions, TypedLobby.Default);

        InitializeLobby(joinGameInput.text);

    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
   

