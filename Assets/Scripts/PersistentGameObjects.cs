using System;
using UnityEngine;

public class PersistentGameObjects : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
