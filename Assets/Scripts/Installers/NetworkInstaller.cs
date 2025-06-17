using Zenject;

public class NetworkInstaller : MonoInstaller<NetworkInstaller>
{
   public override void InstallBindings()
    {
        Container.Bind<INetworkService>().To<PhotonNetworkService>().AsSingle();
    }
}