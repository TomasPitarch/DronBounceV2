using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


public class ScreensInstaller : MonoInstaller
{
    [SerializeField]
    private LoginView loginView;
    
    [SerializeField]
    private InitialView initialView;
    
    
    public override void InstallBindings()
    {
        BindInitialConnect();
        BindLogin();
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

    
}
