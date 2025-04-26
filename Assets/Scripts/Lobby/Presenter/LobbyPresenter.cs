using System.Collections.Generic;

public class LobbyPresenter
{
    private LobbyView _lobbyView;
    private ILobbyService _lobbyService;
    

    public LobbyPresenter(LobbyView lobbyView,ILobbyService lobbyService)
    {
        _lobbyView = lobbyView;
        _lobbyView.OnStartButtonClick += StartGame;
        _lobbyView.OnShow += ShowViewHandler;
        
        _lobbyService=lobbyService;

        _lobbyService.OnPlayerListUpdate += PlayersUpdate;
        _lobbyService.OnBecomeServer += EnableStartCapabilities;
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
        //photonView.RPC("LoadGameScene",RpcTarget.All);
    }
    
}