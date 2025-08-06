using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Zenject;

public class GameManager : MonoBehaviourPunCallbacks,IObservable<GameState>
{
    private readonly List<IObserver<GameState>> _observers = new ();
   
    private BallManager _ballManager;

    [SerializeField]
    private ScoreManager myScoreManager;

    [SerializeField]
    private FreezeCounter freezeCounter;
    
    [SerializeField]
    private PlayerSpawner playerSpawner;

    [SerializeField]
    private int maxPlayerAllowed;
    

    private Dictionary<PlayerOrder,Player> _playersOrders;
    private List<Player> _playersInGame=new();
    private IRoomService _roomService;
    private HudPresenter _hudPresenter;
    
    private int _playersCounter=0;
    
    [Inject]
    public void Initialize(IRoomService roomService,BallManager ballManager, HudPresenter hudPresenter)
    {
        _roomService = roomService;
        _ballManager = ballManager;
        _hudPresenter = hudPresenter;
    }
    
    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        _playersOrders = new ();
        OnPlayerEnteredRoom(PhotonNetwork.LocalPlayer);
        Subscribe(_ballManager);

    }
    
    private void Start()
    {
        photonView.RPC(nameof(ClientReadyRPC), RpcTarget.All);
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        myScoreManager.OnLose += PlayerLose;
        InitialSetup();
    }

    private void InitialSetup()
    {
        photonView.RPC(nameof(SetGameModeRPC), RpcTarget.All);
        RegisterPlayers();
        InitScores();
    }

    #region RPC

    [PunRPC]
    private void SetLoserClientRPC()
    {
        _hudPresenter.ShowLose();
    }

    [PunRPC]
    private void SetWinnerClientRPC()
    {
        _hudPresenter.ShowWin();
    }
    [PunRPC]
    private void SetGameModeRPC()
    {
        SetGameMode();
    }

    [PunRPC]
    private void SpawnPlayerRPC(PlayerOrder playerOrder)
    {
        playerSpawner.SpawnPlayer(playerOrder);
    }
    [PunRPC]
    private void SetPlayerInHudRPC(PlayerOrder playerOrder,Player player)
    {
        _hudPresenter.SetPlayerInHud(playerOrder,player);
    }
    
    [PunRPC]
    private void StartCountDownRPC()
    {
        freezeCounter.StartCounter(5);
    }

    [PunRPC]
    private void ClientReadyRPC()
    {
        _playersCounter++;
        ReadyCheck();
    }

    #endregion

    private async void StartGame()
    {
        photonView.RPC(nameof(StartCountDownRPC), RpcTarget.All);
        await UniTask.Delay(TimeSpan.FromSeconds(4.75));
        NotifyGameStart();
    }
    private void SetGameMode()
    {
        switch (_roomService.GetTypeOfRoom())
        {
            case TypeOfRoom.PvP:
                _hudPresenter.SetGameMode(TypeOfRoom.PvP);
                myScoreManager.SetPvpMap();
                playerSpawner.SetTypeOfRoom(TypeOfRoom.PvP);
                break;
            case TypeOfRoom.AllVsAll:
                _hudPresenter.SetGameMode(TypeOfRoom.AllVsAll);
                playerSpawner.SetTypeOfRoom(TypeOfRoom.AllVsAll);
                myScoreManager.SetAllvsAllMap();
                break;
            default:
                Debug.LogError("Unknown room type");
                break;
        }
    }
    private void PlayerLose(PlayerOrder order)
    {
        myScoreManager.EndTriggerFunction(order);
        _playersInGame.Remove(_playersOrders[order]);
        photonView.RPC(nameof(SetLoserClientRPC),_playersOrders[order]);
        
        WinnerCheck();
    }
    private void WinnerCheck()
    {
        if (_playersInGame.Count==1)
        {
            Player winnerPlayer = _playersInGame.First();
            photonView.RPC(nameof(SetWinnerClientRPC), winnerPlayer);
            EndGame();
        }
    }
    private void ReadyCheck()
    {
        if (_playersCounter == _roomService.GetTypeOfRoom().ToNumberOfPlayers())
        {
            StartGame();
        }
    }
    private void InitScores()
    {
        foreach (var valuePair in _playersOrders)
        {
            myScoreManager.InitScores(valuePair.Key);
        }
    }

    private void RegisterPlayers()
    {
        int i = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            _playersOrders.Add((PlayerOrder)i, player);
            _playersInGame.Add(player);
            photonView.RPC(nameof(SetPlayerInHudRPC), RpcTarget.All, (PlayerOrder)i, player);
            photonView.RPC(nameof(SpawnPlayerRPC), player, (PlayerOrder)i);
            i++;
        }
    }

    private void EndGame()
    {
        NotifyGameEnded();
    }
    
    private void NotifyGameEnded()
    {
        foreach (IObserver<GameState> observer in _observers)
        {
            observer.OnNext(GameState.Ended); 
        }
    }
    
    private void NotifyGameStart()
    {
        foreach (IObserver<GameState> observer in _observers)
        {
            observer.OnNext(GameState.Started); 
        }
    }
    
    private class Unsubscriber<T> : IDisposable
    {
        private List<T> _observers;
        private T _observer;

        public Unsubscriber(List<T> observers, T observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }

    public IDisposable Subscribe(IObserver<GameState> observer)
    {
        _observers.Add(observer);
        return new Unsubscriber<IObserver<GameState>>(_observers,observer);

    }
}




