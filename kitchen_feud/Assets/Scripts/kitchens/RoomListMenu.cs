using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListMenu : MonoBehaviourPunCallbacks
{
    // the find room canvas containing all the rooms
    [SerializeField] private Transform _content;
    // room list prefab 
    [SerializeField] public GameObject _roomListing;


    // Caches the room list so won't only update when seeing lobby menu
    public Dictionary<string, GameObject> cachedRoomList = new Dictionary<string, GameObject>();


    // Photon callback which continually updates the find room menu (if in main menu)
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
            // change room info to current cachedroomlist info
            RoomListItem roomInfo = cachedRoomList[info.Name].GetComponent<RoomListItem>();
            roomInfo.SetUp(info);
        }
        else
        {
            // otherwise add a room to the find lobby canvas with correct info
            GameObject listing = Instantiate(_roomListing, _content);
            RoomListItem roomInfo = listing.GetComponent<RoomListItem>();
            roomInfo.SetUp(info);
            cachedRoomList[info.Name] = listing;
        }
    }

    // Clear cached rooms on disconnect
    public override void OnDisconnected(DisconnectCause cause)
    {
        cachedRoomList.Clear();
    }
}

