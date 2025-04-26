using System;
using System.Collections.Generic;

public interface ILobbyService
{
    public event Action OnBecomeServer;
    public event Action<List<string>> OnPlayerListUpdate;
    public event Action OnStartGame;


    public string RoomName();
}
    
