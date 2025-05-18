using System;
using UnityEngine;

public class ParticleSystemProduct : MonoBehaviour
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
}