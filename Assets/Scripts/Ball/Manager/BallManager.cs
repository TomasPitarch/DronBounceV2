using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


public class BallManager:MonoBehaviour,IObserver<GameState>
{
    private BallSpawner _ballSpawner;

    [Inject]
    public void Initialize(BallSpawner ballSpawner)
    {
        _ballSpawner = ballSpawner;
    }

    private void EndGameHandler()
    {
        //_ballSpawner.EndGame();
        StopAllCoroutines();
    }
    private void StartGameHandler()
    {
        FirstBall();
    }
    private void FirstBall()
    {
        StartCoroutine(nameof(SpawnBall), 0);
    }
    private IEnumerator SpawnBall(int elapsedTime)
    {
        yield return new WaitForSeconds(elapsedTime);
        _ballSpawner.Spawn();
        NextBall();
    }
    private void NextBall()
    {
        // if(liveBalls<maxBalls)
        // {
            int randomInt = Random.Range(3, 6);
            StartCoroutine(nameof(SpawnBall), randomInt);
        // }
    }

    #region Observer
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(GameState value)
    {
        switch (value)
        {
            case GameState.Started:
                StartGameHandler();
                break;
            case GameState.Ended:
                EndGameHandler();
                break;
        }
    }

    #endregion
    
}