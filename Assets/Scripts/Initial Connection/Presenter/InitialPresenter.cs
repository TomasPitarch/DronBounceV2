
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class InitialPresenter
{
    private InitialView _view;
    private INetworkService _networkService;
    private Navigator _navigator;
    
    public InitialPresenter(InitialView view,INetworkService networkService,Navigator navigator)
    {
        _view = view;
        _networkService = networkService;
        _navigator = navigator;
        
        _view.OnConnectButtonClicked += HandleConnectButtonClicked;
        _view.AvailableStateUI();

        _view.OnShow += HandleConnectButtonClicked;

    }

    private void HandleConnectButtonClicked()
    {
        TryConnect();
    }
    private async UniTask TryConnect()
    {
        _view.LoadingStateUI();
        try
        {
            await _networkService.Connect()
                .Timeout(TimeSpan.FromSeconds(10)) 
                .ContinueWith(ConnectSuccessNextScreen);
        }
        catch (TimeoutException)
        {
            ConnectionTimeout();
        }
    }

    private void ConnectionTimeout()
    {
        //TODO:change answer implementation with a popup instead a debug log
        Debug.LogWarning("Connection Timeout");;
        _view.AvailableStateUI();
    }

    private void ConnectSuccessNextScreen()
    {
        //TODO:change implementation without hardcoding string
        _navigator.OpenScreen("Login");
    }
}
