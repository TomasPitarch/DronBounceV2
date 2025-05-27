using UnityEngine;
using Zenject;

public class LobbyInstaller : MonoInstaller<LobbyInstaller>
{
    [SerializeField]
    private PhotonLobbyService lobbyService;
    public override void InstallBindings()
    {
        Container.Bind<ILobbyService>().FromInstance(lobbyService).AsSingle();
    }
}