using UnityEngine;
using Zenject;

public class NetworkInstaller : MonoInstaller<NetworkInstaller>
{
    [SerializeField]
    PhotonLobbyService lobbyService;
    public override void InstallBindings()
    {
        Container.Bind<INetworkService>().To<PhotonNetworkService>().AsSingle();
        Container.Bind<ILoginService>().To<PhotonLoginService>().AsSingle();
        Container.Bind<ILobbyService>().FromInstance(lobbyService).AsSingle();
    }
}

