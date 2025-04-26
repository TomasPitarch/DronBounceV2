using UnityEngine;
using Zenject;


public class ScreensInstaller : MonoInstaller
{
    [SerializeField]
    private LoginView loginView;
    
    [SerializeField]
    private InitialView initialView;
    
    [SerializeField]
    private LobbyView lobbyView;
    
    public override void InstallBindings()
    {
        BindInitialConnect();
        BindLogin();
        BindLobby();
    }
    private void BindLogin()
    {
        Container.Bind<LoginView>().FromInstance(loginView).AsSingle();
        Container.Bind<LoginPresenter>().AsSingle().NonLazy();
    }
    
    private void BindInitialConnect()
    {
        Container.Bind<InitialView>().FromInstance(initialView).AsSingle();
        Container.Bind<InitialPresenter>().AsSingle().NonLazy();
    }
    private void BindLobby()
    {
        Container.Bind<LobbyView>().FromInstance(lobbyView).AsSingle();
        Container.Bind<LobbyPresenter>().AsSingle().NonLazy();
    }

    
}
