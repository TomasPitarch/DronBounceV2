using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager_Client : MonoBehaviourPun
{

    [SerializeField]
    HUDView myHUD;

    
    public void PlayerLose(Player loserPlayer)
    {
        Debug.Log("HUD/Player Lose");
        photonView.RPC(nameof(SetLoserClient),loserPlayer);
    }
    public void PlayerWin(Player winnerPlayer)
    {
        photonView.RPC(nameof(SetWinnerClient), winnerPlayer);
    }

    [PunRPC]
    void SetLoserClient()
    {
        myHUD.LoseHUD();
    }

    [PunRPC]
    void SetWinnerClient()
    {
        myHUD.WinHUD();
    }

    public void ToLoginScene()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Login");
    }
}
