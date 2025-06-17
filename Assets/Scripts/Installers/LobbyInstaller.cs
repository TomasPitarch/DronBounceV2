using UnityEngine;
using Zenject;

public class LobbyInstaller : MonoInstaller<LobbyInstaller>
{
    [SerializeField]
    private PhotonRoomService roomService;
    
    public override void InstallBindings()
    {
        Container.Bind<IRoomService>().FromInstance(roomService).AsSingle();
        Container.Bind<ILobbyService>().To<PhotonLobbyService>().AsSingle();
    }
}