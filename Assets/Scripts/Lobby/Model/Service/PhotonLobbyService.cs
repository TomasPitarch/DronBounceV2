using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLobbyService : ILobbyService,ILobbyCallbacks,IMatchmakingCallbacks
{
    public event Action<List<RoomItem>> OnRoomListUpdated;
    private List<RoomItem> _roomsInfo;
    private readonly RoomOptions _defaultRoomOptions;
    private UniTaskCompletionSource<RoomResponse> _roomConnectionTcs;
    
    private TypeOfRoom _currentTypeOfRoom;

    public PhotonLobbyService()
    {
        PhotonNetwork.AddCallbackTarget(this);
        
        _defaultRoomOptions = new RoomOptions
        {
            IsOpen = true,
            MaxPlayers = 4
        };
    }

    #region ILobbyService

    public UniTask<RoomResponse> Connect(string roomName)
    {
        _roomConnectionTcs = new UniTaskCompletionSource<RoomResponse>();
        PhotonNetwork.JoinRoom(roomName);
        return _roomConnectionTcs.Task;
    }

    public List<RoomItem> GetRooms()
    {
        return _roomsInfo;
    }

    public UniTask<RoomResponse> CreateRoom(string roomName, TypeOfRoom typeOfRoom)
    {
        _currentTypeOfRoom=typeOfRoom;
        _roomConnectionTcs = new UniTaskCompletionSource<RoomResponse>();
        _defaultRoomOptions.MaxPlayers = _currentTypeOfRoom.ToNumberOfPlayers();
        PhotonNetwork.CreateRoom(roomName, _defaultRoomOptions, TypedLobby.Default);
        
        return _roomConnectionTcs.Task;
        
    }

    #endregion
   
    #region ILobbyCallbacks

    public void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }

    public void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomsInfo = new List<RoomItem>();
        foreach (RoomInfo room in roomList)
        {
            RoomItem item = new RoomItem
            {
                RoomName = room.Name,
                ActualPlayers = room.PlayerCount,
                TypeOfRoom = room.MaxPlayers == 2 ? TypeOfRoom.PvP:TypeOfRoom.AllVsAll
            };
            _roomsInfo.Add(item);
        }
        OnRoomListUpdated?.Invoke(_roomsInfo);
    }
    

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log("Lobby Statistics Update");
    }
    #endregion

    #region IMatchmakingCallbacks

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Debug.Log("Friend List Update");
    }

    public void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        RoomResponse response = new RoomResponse(RoomStatus.Created, "Room Created", 200);
        _roomConnectionTcs?.TrySetResult(response);
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        RoomResponse response = new RoomResponse(RoomStatus.Connected,"Room Joined", 200);
        _roomConnectionTcs?.TrySetResult(response);
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
        RoomResponse response = new RoomResponse(RoomStatus.Error, message, returnCode);
        _roomConnectionTcs?.TrySetResult(response);
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        RoomResponse response = new RoomResponse(RoomStatus.Error, message, returnCode);
        _roomConnectionTcs?.TrySetResult(response);
    }

    public void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    #endregion
   
}