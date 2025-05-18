using UnityEngine;
using Zenject;

public class BounceEffectParticleFactory :MonoBehaviour,IFactory<ParticleSystemProduct>
{
    private BallDataSo _data;
    private const string DefaultName = "ParticleSystem";

    [Inject]
    public void InjectDependencies(BallDataSo data)
    {
        _data = data;
    }
    public void Start()
    {
        DontDestroyOnLoad(this);
    }
    public ParticleSystemProduct Create()
    {
        ParticleSystemProduct product=Instantiate(_data.bounceEffect);
        product.gameObject.name = DefaultName;
        
        return product;
    }
}