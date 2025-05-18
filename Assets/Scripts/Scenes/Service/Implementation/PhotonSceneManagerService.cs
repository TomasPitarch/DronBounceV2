using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PhotonSceneManagerService :MonoBehaviourPun,ISceneManagerService
{
    private UniTask _loadSceneCompletionSource;
    private IProgress<float> _progress;

    private readonly List<int> _idReadyList=new ();

   
    public void Start()
    { 
        DontDestroyOnLoad(this);
    }

    #region ISceneManagerService
    public void LoadSceneAsyncAllClients(string sceneName)
    {
        photonView.RPC("LoadSceneAsyncRPC",RpcTarget.All,sceneName);
    }

    public IProgress<float> GetSceneProgress()
    {
        return _progress;
    }
    #endregion

    #region RPC

    [PunRPC]
    private async void LoadSceneAsyncRPC(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //asyncOperation.allowSceneActivation = false;
        // await SceneManager.LoadSceneAsync(sceneName).ToUniTask(_progress);
        _loadSceneCompletionSource= asyncOperation.ToUniTask(_progress);
        await _loadSceneCompletionSource.ContinueWith(ClientLoadSceneComplete);
    }
    [PunRPC]
    private void ClientSceneReadyRPC(int playerId)
    {
        _idReadyList.Add(playerId);
        CheckAllPlayerLoadSceneStatus();
    }
    #endregion
    private void ClientLoadSceneComplete()
    {
        photonView.RPC("ClientSceneReadyRPC",RpcTarget.MasterClient,PhotonNetwork.LocalPlayer.ActorNumber);
    }
    private void CheckAllPlayerLoadSceneStatus()
    {
        Debug.Log("A player end with the charge.");
        if (_idReadyList.Count is 2)
        {
            Debug.Log("All players ready.");
        }
    }
   
}