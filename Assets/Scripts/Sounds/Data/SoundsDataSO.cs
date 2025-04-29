using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundsDataSO", menuName = "Scriptable Objects/SoundsDataSO")]
public class SoundsDataSo : ScriptableObject
{
    public List<AudioData> AudioClips;
    
}

[Serializable]
public class AudioData
{
    public string Id;
    public AudioClip AudioClip;
}
