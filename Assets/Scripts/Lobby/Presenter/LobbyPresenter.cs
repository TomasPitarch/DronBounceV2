using System;
using System.Collections.Generic;


public class LobbyPresenter
{
    private readonly LobbyView _view;
    private readonly ILobbyService _lobbyService;
    private readonly Navigator _navigator;


    private RoomItem _currentRoomSelected;
    

    public LobbyPresenter(LobbyView view, ILobbyService lobbyService,Navigator navigator)
    {
        _view = view;
        _lobbyService = lobbyService;
        _navigator = navigator;
        
        _view.OnRefreshButtonPress += UpdateListOfRooms;
        _view.OnConnectButtonPress += ConnectToRoom;
        _view.OnRoomButtonPress += RoomSelectHandler;
        _view.OnCreateRoomButtonPress += AcceptCreateRoomButtonHandler;
        _view.OnCreateRoomOpenWindowButtonPress += CreateRoomButtonHandler;

        _lobbyService.OnRoomListUpdated+= ListOfRoomsUpdateHandler;
    }

    private void ListOfRoomsUpdateHandler(List<RoomItem> listOfRooms)
    {   //TODO:Should be called when lobby is opened. If a lot of rooms are created, it can be a problem
        _currentRoomSelected = null;
        _view.SetRoomName("");
        _view.UpdateListOfRooms(listOfRooms);
    }

    private async void AcceptCreateRoomButtonHandler(string roomName)
    {
        try
        {
            //TODO:loading feedback and remove hardcode type of room
            await _lobbyService.CreateRoom(roomName, _view.GetTypeOfRoomSelected());
            _navigator.OpenScreen("Login");
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }
    private void CreateRoomButtonHandler()
    {
        _view.OpenCreateRoomWindow();
    }

    private void RoomSelectHandler(RoomItem roomItem)
    {
        _currentRoomSelected = roomItem;
        _view.SetRoomName(_currentRoomSelected.RoomName);
    }
  
    private async void ConnectToRoom()
    {
        try
        {
            if (_currentRoomSelected is null) return;
            await _lobbyService.Connect(_currentRoomSelected.RoomName);
            _navigator.OpenScreen("Login");
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }

    private void UpdateListOfRooms()
    {
        List<RoomItem> rooms = _lobbyService.GetRooms();
        _currentRoomSelected = null;
        _view.SetRoomName("");
        _view.UpdateListOfRooms(rooms);
    }
}