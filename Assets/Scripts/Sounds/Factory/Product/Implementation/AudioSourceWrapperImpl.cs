using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class AudioSourceWrapperImpl : IAudioSourceWrapper, IInitializable, IDisposable
{
    private readonly GameObject _audioObject;
    public AudioSource AudioSource { get; }
    
    public AudioSourceWrapperImpl(GameObject gameObject,AudioSource audioSource)
    {
        _audioObject = gameObject;
        AudioSource = audioSource;
    }

    #region IAudioSourceWrapper
   
    public void Play()
    {
        AudioSource.Play();
    }

    public void Stop()
    {
        AudioSource.Stop();
    }

    public void SetClip(AudioClip clip)
    {
        AudioSource.clip = clip;
    }

    #endregion

    #region IInitializable
    public void Initialize()
    {
        AudioSource.playOnAwake = false;
        AudioSource.spatialBlend = 0f;
    }

    #endregion 
    
    #region IDisposable
    public void Dispose()
    {
        if (_audioObject != null)
        {
            Object.Destroy(_audioObject);
        }
    }

    #endregion
   
}