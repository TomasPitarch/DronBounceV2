using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;


public class PhotonLobbyService : MonoBehaviourPunCallbacks,ILobbyService
{ 
    private readonly List<string> _playersList = new();

    public void  Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
        DontDestroyOnLoad(this);
    }
    #region ILobbyService
    
    public event Action OnBecomeServer;
    public event Action<List<string>> OnPlayerListUpdate;
    public event Action OnStartGame;

    public string RoomName()
    {
        return PhotonNetwork.CurrentRoom.Name;
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
        _playersList.Add(newPlayer.NickName);
        photonView.RPC(nameof(RpcUpdateTeam), RpcTarget.All, ListNicknamesToString(_playersList));
    }
    public override void OnPlayerLeftRoom(Player playerLeft)
    {
        _playersList.Remove(playerLeft.NickName);
        photonView.RPC(nameof(RpcUpdateTeam), RpcTarget.All,ListNicknamesToString(_playersList));
    }

    #endregion

    #region RPCs

    [PunRPC]
    private void RpcUpdateTeam(string nicknames)
    {
        OnPlayerListUpdate?.Invoke(StringToListNicknames(nicknames));
    }
    
    [PunRPC]
    void RpcStartGame() {
        
        OnStartGame?.Invoke();
    }
    

    #endregion
    
    private string ListNicknamesToString(List<string> listOfNickNames)
    {
        string newString="";
    
        foreach (string playerNickname in listOfNickNames)
        {
            newString = newString + "/" + playerNickname;
        }
    
        return newString;
    }
    private List<string> StringToListNicknames(string nickNames)
    {
        return nickNames.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
    }
   
}
