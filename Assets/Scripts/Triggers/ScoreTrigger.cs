using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Zenject;

public class ScoreTrigger : MonoBehaviour
{
    //Events//
    public event Action<int> OnScoreTrigger = delegate { };
    public int Order;

    Collider myCollider;
    Rigidbody myRigidbody;
    MeshRenderer myMeshRenderer;

    [SerializeField]
    Light scoreLight;

    [SerializeField]
    string ScoreSound = "ScoreSound";
    
    private ISoundService _soundService;
    
    [Inject]
    public void Initialize(ISoundService soundService)
    {
        _soundService = soundService;
    }

    private void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        myMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeTriggerToWall()
    {
        myCollider.isTrigger = false;
        myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        myMeshRenderer.enabled = true;
        tag = "Wall";
    }
    public  void LigthOnScore()
    {
        scoreLight.GetComponent<Animator>().SetTrigger("score");
        _soundService.PlaySound(ScoreSound);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var ball = other.GetComponent<Ball>();

            if (ball != null)
            {
                ball.BallRelease();
                OnScoreTrigger(Order);
            }
        }
    }
}
