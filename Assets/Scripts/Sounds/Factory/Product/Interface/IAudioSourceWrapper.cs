using UnityEngine;

public interface IAudioSourceWrapper:IPoolable
{
    AudioSource AudioSource { get; }
    public void Play();
    public void Stop();
    public void SetClip(AudioClip clip);
}