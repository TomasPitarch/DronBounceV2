using System.Collections.Generic;

public class RoomPresenter
{
    private readonly RoomView _roomView;
    private readonly IRoomService _roomService;
    private readonly ISceneManagerService _sceneManagerService;
    
    private bool _isServer=false;
    

    public RoomPresenter(RoomView roomView,IRoomService roomService,ISceneManagerService sceneManagerService)
    {
        _roomView = roomView;
        _roomView.OnStartButtonClick += StartGame;
        _roomView.OnShow += ShowViewHandler;
        
        _roomService=roomService;

        _roomService.OnPlayerListUpdate += PlayersUpdate;
        _roomService.OnBecomeServer += SetClientAsServer;

        _sceneManagerService = sceneManagerService;
    }

    private void ShowViewHandler()
    {
        _roomView.SetRoomName(_roomService.RoomName());
        _roomView.SetTypeOfRoom(_roomService.GetTypeOfRoom());
    }

    private void SetClientAsServer()
    {
        _isServer = true;
        _roomView.ActiveStartButton();
    }

    private void PlayersUpdate(List<string> players)
    {
       _roomView.SetPlayersNames(players);
       int numberOfPlayers = _roomService.GetTypeOfRoom().ToNumberOfPlayers();
       if ( _isServer && players.Count == numberOfPlayers)
       {
            _roomView.EnableStartButton();
       }
       else
       {
           _roomView.DisableStartButton();
       }
    }

    private void StartGame()
    {
        _sceneManagerService.LoadSceneAsyncAllClients("Game");
    }
    
}