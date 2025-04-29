using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundInstaller : MonoInstaller<SoundInstaller>
{
    [SerializeField] 
    private List<SoundsDataSo> soundsData;

    [SerializeField] 
    private GameObjectFactory gameObjectFactoryPrefab;
    
    public override void InstallBindings()
    {
        GameObjectFactory gameObjectFactory = Instantiate(gameObjectFactoryPrefab);
        gameObjectFactory.name = "Game Object Factory";

        GameObject audioSourceParent = new GameObject();
        audioSourceParent.name = "Audio Sources";
        Transform audioSourceTransform = audioSourceParent.transform;
        
        
        Container.Bind<IFactory<IAudioSourceWrapper>>().
            To<AudioSourceFactory>().AsSingle().WithArguments(gameObjectFactory, audioSourceTransform);

        Container.Bind<ObjectPool<IAudioSourceWrapper>>().AsTransient();
        
        
        Container.Bind<ISoundService>().To<SoundService>().AsSingle();
        Container.Bind<List<SoundsDataSo>>().FromInstance(soundsData).AsSingle();
        
    }
}