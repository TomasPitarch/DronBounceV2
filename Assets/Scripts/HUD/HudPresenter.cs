using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class HudPresenter
{
    private ScoreManager _scoreManager;
    
    private HUDView _view;
    private IRoomService _roomService;
    
    public HudPresenter(HUDView view,
         ScoreManager scoreManager, IRoomService roomService)
    {
        _view = view;
        _view.OnShow += SetRoomName;
        
        
        _scoreManager = scoreManager;
        _roomService = roomService;
        
        // Subscribe to events
        scoreManager.OnScoreGoal += UpdateScoreHandler;
        
    }

    private void SetRoomName()
    {
        string roomName = _roomService.RoomName();
        _view.SetRoomName(roomName);
    }

    public void SetPlayerInHud(PlayerOrder playersOrders,Player player)
    {
        _view.SetPlayerName(playersOrders,player.NickName);
        _view.SetPlayerScore(playersOrders, _scoreManager.GetGoal());
    }

    private void UpdateScoreHandler(PlayerOrder playerOrder, int newScore)
    {     
        _view.SetPlayerScore(playerOrder, newScore);
        
    }
    
    public void SetGameMode(TypeOfRoom typeOfRoom)
    
    {
        switch (typeOfRoom)
        {
            case TypeOfRoom.PvP:
                _view.SetPvsPHud();
                break;
            case TypeOfRoom.AllVsAll:
                _view.SetAllVsAllHud();
                break;
            default:
                Debug.LogError("Unknown room type");
                break;
        }
    }
    public void ShowLose()
    {
        _view.ShowLoseHud();
    }
    public void ShowWin()
    {
        _view.ShowWinHud();
    }
}
