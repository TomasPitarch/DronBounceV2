using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] 
    private PhotonSceneManagerService sceneManagerService;
    public override void InstallBindings()
    {
        Container.Bind<ISceneManagerService>().FromInstance(sceneManagerService).AsSingle();
    }
}