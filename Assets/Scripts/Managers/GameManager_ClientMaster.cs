using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Zenject;

public class GameManager_ClientMaster : MonoBehaviourPunCallbacks,IObservable<GameState>
{
    private readonly List<IObserver<GameState>> _observers = new ();
    
    GameManager_Client GM_Client;

    
    private BallManager _ballManager;

    [SerializeField]
    public ScoreManager_ClientMaster myScoreManager;

    [SerializeField]
    FreezeCounter _freezeCounter;


    [SerializeField]
    int MaxPlayerAllowed;

    public Dictionary<int,Player> PlayersOrders;
    private List<Player> ActivePlayers;

    [Inject]
    public void Initialize(BallManager ballManager)
    {
        _ballManager = ballManager;
    }
    void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(this);
        }
        GM_Client = GetComponent<GameManager_Client>();
        PlayersOrders = new Dictionary<int, Player>();
        ActivePlayers = new List<Player>();
        OnPlayerEnteredRoom(PhotonNetwork.LocalPlayer);
        Subscribe(_ballManager);

    }
    void Start()
    {
        myScoreManager.OnLose += PlayerLose;
        StartGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    
   

    void PlayerLose(int order)
    {
        myScoreManager.EndTriggerFunction(order);

        if (PlayersOrders.ContainsKey(order))
        {
            Player loserPlayer = PlayersOrders[order];
            GM_Client.PlayerLose(loserPlayer);

            ActivePlayers.Remove(loserPlayer);
        }

        WinnerCheck();

    }

    void WinnerCheck()
    {
        Debug.Log("winnercheck");

        if (ActivePlayers.Count==1)
        {
            Player winnerPlayer = ActivePlayers[0];
            GM_Client.PlayerWin(winnerPlayer);
            Debug.Log("We have a winner");
            EndGame();
        }
    }

    async void StartGame()
    {
        try
        {
            await _freezeCounter.StartCounter(5);
            RegisterPlayers();
            NotifyGameStart();
            //TODO:suscribe score manager to listen the start notify
            myScoreManager.SetInitialScores();
        }
        catch (Exception e)
        {
            throw e; // TODO handle exception
        }
    }

    private void RegisterPlayers()
    {
        int i = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            PlayersOrders.Add(i, player);

            ActivePlayers.Add(player);
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
    
    internal class Unsubscriber<T> : IDisposable
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
