using UnityEngine;

public class GameObjectFactory : MonoBehaviour, IFactory<GameObject> 
{
    private const string DefaultName = "DynamicAudioSource";

    public GameObject Create()
    {
        return new GameObject(DefaultName);
    }
}