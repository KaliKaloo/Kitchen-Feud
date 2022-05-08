using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _content;

    [SerializeField] public GameObject _roomListing;

    public Dictionary<string, GameObject> cachedRoomList = new Dictionary<string, GameObject>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (roomList[i].IsOpen && roomList[i].IsVisible)
            {
                if (info.RemovedFromList)
                {
                    if (cachedRoomList.ContainsKey(info.Name))
                    {
                        Destroy(cachedRoomList[info.Name]);
                        cachedRoomList.Remove(info.Name);
                    }
                }
                else
                {
                    if (cachedRoomList.ContainsKey(info.Name) && cachedRoomList[info.Name] != null)
                    {
                        RoomListItem roomInfo = cachedRoomList[info.Name].GetComponent<RoomListItem>();
                        roomInfo.SetUp(info);
                    }
                    else
                    {
                        GameObject listing = Instantiate(_roomListing, _content);
                        RoomListItem roomInfo = listing.GetComponent<RoomListItem>();
                        roomInfo.SetUp(info);
                        cachedRoomList[info.Name] = listing;
                    }
                }
            }
        }
    }

    public override void OnJoinedRoom()
    {
        RoomInfo info = PhotonNetwork.CurrentRoom;
        if (cachedRoomList.ContainsKey(info.Name) && cachedRoomList[info.Name] != null)
        {
            RoomListItem roomInfo = cachedRoomList[info.Name].GetComponent<RoomListItem>();
            roomInfo.SetUp(info);
        }
        else
        {
            GameObject listing = Instantiate(_roomListing, _content);
            RoomListItem roomInfo = listing.GetComponent<RoomListItem>();
            roomInfo.SetUp(info);
            cachedRoomList[info.Name] = listing;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        cachedRoomList.Clear();
    }
}

