using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Pool;
using Zenject;

public class Ball : MonoBehaviourPun
{
    public event Action<Ball> OnBallRelease;
    
    [SerializeField]
    private Rigidbody _rigidbody;

    private BallDataSo _ballData;
    private ISoundService _soundService;
    private ObjectPool<ParticleSystemProduct> _sparkObjectPool;
    
    [Inject]
    public void InjectDependencies(ISoundService soundService,BallDataSo ballData,ObjectPool<ParticleSystemProduct> sparkObjectPool)
    {
        _soundService = soundService;
        _ballData = ballData;
        _sparkObjectPool = sparkObjectPool;
    }
  
    private void Awake()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            SphereCollider collider = GetComponent<SphereCollider>();
            if(collider!=null)
            {
                Destroy(GetComponent<SphereCollider>());
            }
            
        }
    }
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _ballData.initialForce;
        }
    }
    public void Init(Vector3 startPosition)
    {
        photonView.RPC(nameof(InitRPC),RpcTarget.All,startPosition);
    }
    private Vector3 GetRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f),
                           0,
                           UnityEngine.Random.Range(-1.0f, 1.0f)
                           );
    }
    public void DestroyBall()
    {
        photonView.RPC(nameof(BallReleaseRPC),RpcTarget.All);  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        photonView.RPC(nameof(CollisionEffectRPC),RpcTarget.All);
    }
    private void OnCollisionExit(Collision collision)
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (!collision.gameObject.CompareTag("Floor")) return;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
    }
    private void InitializeBall(Vector3 startPosition)
    {
        gameObject.transform.position = startPosition;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.linearVelocity = (GetRandomDirection() * _ballData.initialForce);
        gameObject.SetActive(true);
    }
    private void BallRelease()
    {
        OnBallRelease?.Invoke(this);
        gameObject.SetActive(false);
    }
    private void CollisionEffect()
    {
        ParticleSystemProduct spark;
        GetAndSetSpark().PlayParticleSystem();
        _soundService.PlaySound(_ballData.bounceSound);
        return;

        void ReleaseParticle()
        {
            _sparkObjectPool.Release(spark); 
        }
        ParticleSystemProduct GetAndSetSpark()
        {
            spark = _sparkObjectPool.Get().GetComponent<ParticleSystemProduct>();
        
            spark.transform.position = transform.position;
            spark.transform.rotation = Quaternion.identity;
        
            spark.OnParticleSystemStop -= ReleaseParticle;
            spark.OnParticleSystemStop += ReleaseParticle;
            return spark;
        }
    }

    #region RPCs
    [PunRPC]
    private void CollisionEffectRPC()
    {
        CollisionEffect();
    }
   
    [PunRPC]
    private void BallReleaseRPC()
    {
        BallRelease();
    }
    
    [PunRPC]
    private void InitRPC(Vector3 startPosition)
    {
     InitializeBall(startPosition);  
    }
    #endregion

    
}
