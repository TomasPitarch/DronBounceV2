using System;
using UnityEngine;

public class GameObjectFactory : MonoBehaviour, IFactory<GameObject> 
{
    private const string DefaultName = "DynamicGameObject";

    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    public GameObject Create()
    {
        return new GameObject(DefaultName);
    }
}