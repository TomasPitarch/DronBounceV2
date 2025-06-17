using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public interface IRoomService
{
    public event Action OnBecomeServer;
    public event Action<List<string>> OnPlayerListUpdate;
    public event Action OnStartGame;


    public string RoomName();
    public TypeOfRoom GetTypeOfRoom();
    public AuthenticateResponse AuthenticateName(string playerName)
    {
        var auxiliar = PhotonNetwork.PlayerList;
        Debug.Log("Autenticacion");
        
        if (PhotonNetwork.PlayerList.ToList().Any(player => player.NickName.Normalize() == playerName.Normalize()))
        {
            return AuthenticateResponse.NameAlreadyExists;
        }
        else
        {
            PhotonNetwork.NickName = playerName;
           return AuthenticateResponse.Accepted;
        }
    }
}
    
