using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface ILobbyService
{
    public event Action<List<RoomItem>> OnRoomListUpdated;
    UniTask<RoomResponse> Connect(string roomName);
    List<RoomItem> GetRooms();
    UniTask<RoomResponse> CreateRoom(string roomName, TypeOfRoom typeOfRoom);
}