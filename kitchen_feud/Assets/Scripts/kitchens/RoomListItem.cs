using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;


// Class which displays relevant Photon room info 
public class RoomListItem : MonoBehaviour
{
    // Drag text of room prefab here
    [SerializeField] private new Text name;

    RoomInfo info;

    // Turn room text into format ===> Lobby Name: Players / Max Players
    public void SetUp(RoomInfo currentInfo)
    {
        info = currentInfo;
        name.text = currentInfo.Name + " : " + currentInfo.PlayerCount + "/" + currentInfo.MaxPlayers;
    }

    // Joins the game with currently stored info using Photon
    public void OnClick()
    {
        menuController.Instance.JoinGameWithInfo(info);
    }
}
