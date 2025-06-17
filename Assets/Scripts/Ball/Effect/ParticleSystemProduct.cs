using System;
using UnityEngine;

public class ParticleSystemProduct : MonoBehaviour,IPoolable
{
    [SerializeField] private ParticleSystem _particleSystem;
    
    public event Action OnParticleSystemStop;

    public void PlayParticleSystem()
    {
        _particleSystem.Play();
    }
    private void OnParticleSystemStopped()
    {
        OnParticleSystemStop?.Invoke();
    }

    public void OnRelease()
    {
        _particleSystem.Stop();
    }
}