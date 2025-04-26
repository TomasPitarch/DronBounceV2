using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LobbyView :ScreenUI
{
    public event Action OnStartButtonClick;

    private string emptySlotString = "---Slot {0}---";
    
    
    [SerializeField]
    private List<TextMeshProUGUI> listOfNickNames;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private TextMeshProUGUI roomName;
    

    public void Start()
    {
        startButton.onClick.AddListener(StartButtonHandler);
    }
    
    private void StartButtonHandler()
    {
        OnStartButtonClick?.Invoke();
    }
    
    public void SetPlayersNames(List<string> newListOfNickNames)
    {
        for(int i=0;i<listOfNickNames.Count;i++)
        {
            int difference = i - newListOfNickNames.Count;
            if (difference < 0)
            {
                listOfNickNames[i].text = newListOfNickNames[i];
            }
            else
            {
                listOfNickNames[i].text = string.Format(emptySlotString, i + 1);
            }
           
            
        }
    }
    
    public void SetRoomName(string newRoomName)
    {
        roomName.text=newRoomName;
    }
    
    public void EnableStartButton()
    {
        startButton.gameObject.SetActive(true);
    }
    
}