using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomService : MonoBehaviourPunCallbacks,IRoomService
{ 
    public static PhotonRoomService Instance{ get; private set;}
    
    private readonly Dictionary<int,string> _players = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void  Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    #region IRoomService
    
    public event Action OnBecomeServer;
    public event Action<List<string>> OnPlayerListUpdate;
    public event Action OnStartGame;

    public string RoomName()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }

    public TypeOfRoom GetTypeOfRoom()
    {
        return PhotonNetwork.CurrentRoom.MaxPlayers.ToTypeOfRoom();
    }

   
    public AuthenticateResponse AuthenticateName(string playerName)
    {
        if (PhotonNetwork.PlayerList.ToList().Any(player => player.NickName.Normalize() == playerName.Normalize()))
        {
            return AuthenticateResponse.NameAlreadyExists;
        }
        else
        {
            PhotonNetwork.NickName = playerName;
            photonView.RPC(nameof(RegisterPlayerRPC),RpcTarget.MasterClient,PhotonNetwork.LocalPlayer.ActorNumber,playerName);
            
            return AuthenticateResponse.Accepted;
        }
    }
    
    #endregion

    #region MonoBehaviourPunCallbacks
    public override void OnCreatedRoom()
    {
        OnBecomeServer?.Invoke();
    }
    public override void OnJoinedRoom()
    {
        //TODO:this consideration is optimal if never change the master client
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
        else
        {
            OnPlayerEnteredRoom(PhotonNetwork.LocalPlayer);
        }
      
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //TODO:this works when the player register after the room is created/joined
         _players.Add(newPlayer.ActorNumber, "");
    }
    public override void OnPlayerLeftRoom(Player playerLeft)
    {
        _players.Remove(playerLeft.ActorNumber);
        photonView.RPC(nameof(RpcUpdateTeam), RpcTarget.All,DictionaryPlayersToStringNames(_players));
    }

    #endregion

    #region RPCs

    [PunRPC]
    private void RpcUpdateTeam(string nicknames)
    {
        OnPlayerListUpdate?.Invoke(StringToListNicknames(nicknames));
    }
    
    [PunRPC]
    void RpcStartGame() 
    {
        OnStartGame?.Invoke();
    }
    [PunRPC]
    private void RegisterPlayerRPC(int actorNumber,string playerName)
    {
        _players[actorNumber] = playerName;
        photonView.RPC(nameof(RpcUpdateTeam), RpcTarget.All, DictionaryPlayersToStringNames(_players));
    }

    #endregion
    
    private string DictionaryPlayersToStringNames(Dictionary<int,string> listOfNickNames)
    {
        string newString="";
    
        foreach (KeyValuePair<int,string> playerNickname in listOfNickNames)
        {
            newString = newString + "/" + playerNickname.Value;
        }
    
        return newString;
    }
    private List<string> StringToListNicknames(string nickNames)
    {
        return nickNames.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
    }
   
}
