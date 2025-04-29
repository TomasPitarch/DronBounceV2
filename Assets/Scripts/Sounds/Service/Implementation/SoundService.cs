using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class SoundService:ISoundService
{
    private readonly Dictionary<string,AudioClip> _audioDictionary=new ();
    private readonly ObjectPool<IAudioSourceWrapper> _audioSourcePool;
    private readonly CancellationTokenSource _cancellationTokenSource = new ();

    

    public SoundService(List<SoundsDataSo> soundsData,ObjectPool<IAudioSourceWrapper> audioSourcePool)
    {
        _audioSourcePool = audioSourcePool;
        _audioSourcePool.Configure(0,10);
        foreach (SoundsDataSo soundsDataSo in soundsData)
        {
            foreach (AudioData audioData in soundsDataSo.AudioClips)
            {
                _audioDictionary.Add(audioData.Id,audioData.AudioClip);
            }
        }
    }
    
    public async UniTask PlaySound(string audioId)
    {
        AudioClip audioFounded = _audioDictionary[audioId];
        if (audioFounded is null)
        {
            Debug.Log("Audio: "+audioId+" not found");
            return;
        }

        IAudioSourceWrapper audioSource = _audioSourcePool.Get();
        audioSource.SetClip(audioFounded);
        audioSource.Play();
        await WaitUntilAudioFinished(audioSource.AudioSource, _cancellationTokenSource.Token);
        _audioSourcePool.Release(audioSource);

    }
    
    private async UniTask WaitUntilAudioFinished(AudioSource audioSource, CancellationToken cancellationToken)
    {
        await UniTask.WaitUntil(() => !audioSource.isPlaying, cancellationToken: cancellationToken);
    }
    
}