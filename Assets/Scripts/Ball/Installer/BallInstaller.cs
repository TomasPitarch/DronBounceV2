using UnityEngine;
using Zenject;

public class BallInstaller : MonoInstaller
{
    [SerializeField]
    private BallManager ballManager;
    
    [SerializeField]
    private PhotonBallFactory ballFactory;
    
    [SerializeField]
    private BounceEffectParticleFactory gameObjectParticleFactory;
    
    [SerializeField] 
    private BallDataSo ballData;
    public override void InstallBindings()
    {
        Container.Bind<IFactory<ParticleSystemProduct>>().FromInstance(gameObjectParticleFactory).AsSingle();
        Container.Bind<ObjectPool<ParticleSystemProduct>>().AsTransient();
        
        
        Container.Bind<IFactory<Ball>>().To<PhotonBallFactory>().FromInstance(ballFactory).AsSingle();
        Container.Bind<BallSpawner>().AsSingle().WithArguments(ballManager.transform.position);
        
        Container.Bind<ObjectPool<Ball>>().AsTransient();

        
        Container.Bind<BallManager>().FromInstance(ballManager).AsSingle();
        
        Container.Bind<BallDataSo>().FromInstance(ballData).AsSingle();
     
    }
    
}