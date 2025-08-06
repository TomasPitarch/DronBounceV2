using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class RoomView :ScreenUI
{
    public event Action OnStartButtonClick;

    private const string EmptySlotString = "---Slot {0}---";
    private const string LobbyNameString = "LobbyRoom:{0}";


    [SerializeField]
    private List<TextMeshProUGUI> listOfNickNames;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private TextMeshProUGUI roomName;


    private TypeOfRoom _getTypeOfRoom;
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
                if (i >= _getTypeOfRoom.ToNumberOfPlayers())
                {
                    listOfNickNames[i].gameObject.SetActive(false);
                }
                else
                {
                    listOfNickNames[i].text = string.Format(EmptySlotString, i + 1);
                }
            }
            
        }
    }
    
    public void SetRoomName(string newRoomName)
    {
        roomName.text=string.Format(LobbyNameString,newRoomName);
    }
    
    public void ActiveStartButton()
    {
        startButton.gameObject.SetActive(true);
        startButton.interactable = false;
    }
    public void DisableStartButton()
    {
        startButton.interactable = false;
    }
    public void EnableStartButton()
    {
        startButton.interactable = true;
    }
    

    public void SetTypeOfRoom(TypeOfRoom getTypeOfRoom)
    {
        _getTypeOfRoom = getTypeOfRoom;
    }
}