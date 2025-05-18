using Photon.Pun;
using UnityEngine;
using Zenject;

public class PhotonBallFactory : IFactory<Ball>
{
    private readonly DiContainer _container;
    
    private readonly string _ballPrefabName;

    public PhotonBallFactory(string ballPrefabName,DiContainer container)
    {
        _ballPrefabName=ballPrefabName;
        _container = container;
    }
    public Ball Create()
    {
        Ball ball = PhotonNetwork.Instantiate(_ballPrefabName, Vector3.zero, Quaternion.identity).GetComponent<Ball>();
        _container.Inject(ball);

        return ball;
    }
}