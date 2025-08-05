using UnityEngine;
using System;
using System.Collections.Generic;
using Photon.Pun;



public class ScoreManager : MonoBehaviourPun
{
    //Events//
    public event Action<PlayerOrder, int> OnScoreGoal ;
    public event Action<PlayerOrder> OnLose ;

    [SerializeField]
    private List<ScoreTrigger> triggerList;
    
    private Dictionary<PlayerOrder,int> _scoresBinds = new();

    [SerializeField]
    private GameData_SO GameData;
    
    private int _defaultGoal;
   
    private void Start()
    {   
        TriggerListSubscription();
        _defaultGoal = GameData.ScoreToLose;
    }
    
    public void InitScores(PlayerOrder order)
    {
        _scoresBinds.Add(order,_defaultGoal);
    }
    
    private void TriggerListSubscription()
    {
        foreach (ScoreTrigger trigger in triggerList)
        {
            trigger.OnScoreTrigger += ScoreGoal;
        }
    }

    public void EndTriggerFunction(PlayerOrder order)
    {
        photonView.RPC(nameof(EndTriggerRPC), RpcTarget.All, order);
    }
   
    private void ScoreGoal(PlayerOrder playerOrder)
    {
        int newScore = _scoresBinds[playerOrder] -1;

        if(newScore<=0)
        {
            OnLose?.Invoke(playerOrder);
        }
       
        _scoresBinds[playerOrder] = newScore;

        photonView.RPC(nameof(LightsOnScoreRPC),RpcTarget.All,playerOrder);
        photonView.RPC(nameof(ScoreGoalRPC), RpcTarget.All, playerOrder, newScore);
        
    }

    #region RPC

    [PunRPC]
    private void ScoreGoalRPC(PlayerOrder playerOrder, int newScore)
    {
        OnScoreGoal?.Invoke(playerOrder, newScore);
    }
    [PunRPC]
    public void EndTriggerRPC(PlayerOrder order)
    {
        triggerList[(int)order].ChangeTriggerToWall();
    }

    [PunRPC]
    public void LightsOnScoreRPC(PlayerOrder order)
    {
        triggerList[(int)order].LigthOnScore();
    }
   

    #endregion

    public void SetPvpMap()
    {
        triggerList[1].ChangeTriggerToWall();
        triggerList[3].ChangeTriggerToWall();
        triggerList[2].PlayerOrder= PlayerOrder.Player2;
    }

    public void SetAllvsAllMap()
    {
        
    }
    
    public int GetGoal()
    {
        return _defaultGoal;
    }
    
}
