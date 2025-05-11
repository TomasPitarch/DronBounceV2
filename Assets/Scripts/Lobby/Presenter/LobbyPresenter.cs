using System.Collections.Generic;

public class LobbyPresenter
{
    private LobbyView _lobbyView;
    private ILobbyService _lobbyService;
    private ISceneManagerService _sceneManagerService;
    

    public LobbyPresenter(LobbyView lobbyView,ILobbyService lobbyService,ISceneManagerService sceneManagerService)
    {
        _lobbyView = lobbyView;
        _lobbyView.OnStartButtonClick += StartGame;
        _lobbyView.OnShow += ShowViewHandler;
        
        _lobbyService=lobbyService;

        _lobbyService.OnPlayerListUpdate += PlayersUpdate;
        _lobbyService.OnBecomeServer += EnableStartCapabilities;

        _sceneManagerService = sceneManagerService;
    }

    private void ShowViewHandler()
    {
        _lobbyView.SetRoomName(_lobbyService.RoomName());
    }

    private void EnableStartCapabilities()
    {
        _lobbyView.EnableStartButton();
    }

    private void PlayersUpdate(List<string> players)
    {
        _lobbyView.SetPlayersNames(players);
    }

    public void StartGame()
    {
        _sceneManagerService.LoadSceneAsyncAllClients("Game");
    }
    
}