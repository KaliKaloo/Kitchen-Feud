using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{

    [SerializeField] private new Text name;

    RoomInfo info;

    public void SetUp(RoomInfo currentInfo)
    {
        info = currentInfo;
        name.text = currentInfo.Name + " : " + currentInfo.PlayerCount + "/" + currentInfo.MaxPlayers;
    }

    public void OnClick()
    {
        menuController.Instance.JoinGameWithInfo(info);
    }
}
