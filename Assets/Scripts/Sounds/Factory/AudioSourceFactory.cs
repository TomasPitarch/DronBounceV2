using UnityEngine;
using Zenject;

public class AudioSourceFactory:IFactory<IAudioSourceWrapper>
{
    private readonly IFactory<GameObject> _gameObjectFactory;
    private readonly Transform _audioParent;
    
    [Inject(Id = "AudioSourceParent")]
    public AudioSourceFactory(IFactory<GameObject> gameObjectFactory,Transform audioParent)
    {
        _gameObjectFactory = gameObjectFactory;
        _audioParent = audioParent;
    }

    public IAudioSourceWrapper Create()
    {
        GameObject newGameObject=_gameObjectFactory.Create();
        AudioSource audioSource = newGameObject.AddComponent<AudioSource>();
        newGameObject.transform.SetParent(_audioParent.transform);
        IAudioSourceWrapper audioSourceWrapper = new AudioSourceWrapperImpl(newGameObject,audioSource);
        
       
        return audioSourceWrapper;
    }
   
}