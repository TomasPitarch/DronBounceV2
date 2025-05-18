using UnityEngine;


public class BallSpawner
{
    private readonly ObjectPool<Ball> _objectPool;
    private readonly Vector3 _spawnPosition;


    public BallSpawner(Vector3 position, ObjectPool<Ball> objectPool)
    {
        _objectPool=objectPool;
        _spawnPosition = position;
    }

    public void Spawn()
    {
        Ball ball = _objectPool.Get();
        
        ball.OnBallRelease+=RecycleBall;
        ball.Init(_spawnPosition);
    }

    private void RecycleBall(Ball ball)
    {
        ball.OnBallRelease-=RecycleBall;                
        _objectPool.Release(ball);
    }
    
}
