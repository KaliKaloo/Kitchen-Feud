using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class menuController : MonoBehaviourPunCallbacks
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


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }



    private string GetPlayers()
    {
        string players = "Players:" + System.Environment.NewLine;
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            players += player.NickName + System.Environment.NewLine;
        }
        return players;
    }

    /*private void InitializeLobby(string name)
    {
        connectPanel.SetActive(false);
        lobbyMenu.SetActive(true);
        lobbyName.text = "Lobby: " + name;
        playerList.text = GetPlayers();
    }
   */
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected");
    }

    public void ChangeUsernameInput()
    {
        if (usernameInput.text.Length >= 1)
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
        PhotonNetwork.NickName = usernameInput.text;
        greetingMenu.text = "Welcome " + usernameInput.text + "!";
    }

    public void CreateGame()
    {
        // CREATE LOBBY HERE
        PhotonNetwork.CreateRoom(createGameInput.text);


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


        // JOIN EXISTING LOBBY HERE
        PhotonNetwork.JoinRoom(joinGameInput.text);



    }


    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}


