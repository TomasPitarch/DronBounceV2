using Photon.Pun;
using UnityEngine;
using Zenject;

public class PhotonBallFactory : MonoBehaviour,IFactory<Ball>
{
    private DiContainer _container;
    
    [SerializeField] 
    private Ball ballPrefab;
    
    [SerializeField]
    private BallInstantiateDataBusSo ballInstantiateDataBusSo;
    
    private void Awake()
    {
        ballInstantiateDataBusSo.OnInstantiateBall += InjectDependencies;
    }
    [Inject]
    public void InjectDependencies(DiContainer container)
    {
        _container = container;
    }
   
    public Ball Create()
    {
        Ball ball = PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<Ball>();
       
        return ball;
    }
   
    private void InjectDependencies(Ball ball)
    {
        _container.Inject(ball.GetComponent<Ball>());
    }
    
}