using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDView : MonoBehaviour
{
    public event Action OnShow;
    public event Action OnReLogButtonClicked;
    
    [SerializeField]
    private TextMeshProUGUI roomText;

    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private Button reLogButton;

    [SerializeField]
    private List<ScoreItemUI> scoreItemsUIList;


    private Dictionary<PlayerOrder, ScoreItemUI> _playerOrderBind = new();
    
    private void Start()
    {
        resultText.gameObject.SetActive(false);
        reLogButton.onClick.AddListener(ReLogButtonClicked);
    }
    
    public void InitializeHud(TypeOfRoom typeOfRoom,string roomName,int defaultScore )
    {
        SetRoomName(roomName);
        switch (typeOfRoom)
        {
            case TypeOfRoom.PvP:
                SetPvsPHud();
                break;
            case TypeOfRoom.AllVsAll:
                SetAllVsAllHud();
                break;
            default:
                Debug.LogError("Unknown room type");
                break;
        }

        foreach (var pair in _playerOrderBind)
        {
            pair.Value.SetPlayerScore(defaultScore);
        }
    }

    private void ReLogButtonClicked()
    {
        OnReLogButtonClicked?.Invoke();
    }
    public void SetRoomName(string roomName)
    {
        roomText.text = "Room: " + roomName;
    }
    public void SetPlayerScore(PlayerOrder playerOrder, int score)
    {
        if (_playerOrderBind.TryGetValue(playerOrder, out ScoreItemUI scoreItemUI))
        {
            scoreItemUI.SetPlayerScore(score);
        }
        else
        {
            Debug.LogError($"PlayerOrder {playerOrder} not found in _playerOrderBind");
        }
    }
    public void SetPlayerName(PlayerOrder playerOrder, string playerName)
    {
        if (_playerOrderBind.TryGetValue(playerOrder, out ScoreItemUI scoreItemUI))
        {
            scoreItemUI.SetPlayerName(playerName);
        }
        else
        {
            Debug.LogError($"PlayerOrder {playerOrder} not found in _playerOrderBind");
        }
    }
   
    public void ShowWinHud()
    {
        resultText.gameObject.SetActive(true);
        resultText.text = "You Win";
        resultText.color = Color.green;

        reLogButton.gameObject.SetActive(true);


    }
    public void ShowLoseHud()
    {
        resultText.gameObject.SetActive(true);
        resultText.text = "You Lose";
        resultText.color = Color.red;

        reLogButton.gameObject.SetActive(true);
    }

    public void SetPvsPHud()
    {
        _playerOrderBind.Clear();
        
        _playerOrderBind.Add(PlayerOrder.Player1,scoreItemsUIList[0]);
        _playerOrderBind.Add(PlayerOrder.Player2,scoreItemsUIList[1]);

        scoreItemsUIList[0].TurnOn();
        scoreItemsUIList[1].TurnOn();
        scoreItemsUIList[2].TurnOff();
        scoreItemsUIList[3].TurnOff();
        
    }
    public void SetAllVsAllHud()
    {
        _playerOrderBind.Clear();
        
        int i = 0;
        foreach (ScoreItemUI itemUI in scoreItemsUIList)
        {
            _playerOrderBind.Add((PlayerOrder)i,itemUI);
            scoreItemsUIList[i].TurnOn();
            i++;
        }
    }
    public void OnEnable()
    {
        OnShow?.Invoke();
    }
   
   
}