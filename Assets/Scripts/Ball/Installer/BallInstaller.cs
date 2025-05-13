using UnityEngine;
using Zenject;

public class BallInstaller : MonoInstaller
{
    [SerializeField]
    private BallManager ballManager;

    [SerializeField] 
    private Ball ballPrefab;

    [SerializeField] 
    private BallDataSo ballData;
    public override void InstallBindings()
    {
        Container.Bind<IFactory<Ball>>().To<PhotonBallFactory>().AsSingle().WithArguments(ballPrefab.gameObject.name);
        Container.Bind<BallSpawner>().AsSingle().WithArguments(ballManager.transform.position);
        
        Container.Bind<ObjectPool<Ball>>().AsTransient();

        
        Container.Bind<BallManager>().FromInstance(ballManager).AsSingle();
        
        Container.Bind<BallDataSo>().FromInstance(ballData).AsSingle();
     
    }
    
}