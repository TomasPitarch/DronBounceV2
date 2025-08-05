using Zenject;

public class LobbyInstaller : MonoInstaller<LobbyInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ILobbyService>().To<PhotonLobbyService>().AsSingle();
        
        PhotonRoomService roomServiceInstance=PhotonRoomService.Instance;
        Container.Bind<IRoomService>() 
            .FromInstance(roomServiceInstance)
            .AsSingle() 
            .NonLazy();
    }
}