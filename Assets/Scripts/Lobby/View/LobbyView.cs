using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 using UnityEngine.UI;

public class LobbyView : ScreenUI,IFactory<RoomItemButton>
{
    public event Action OnConnectButtonPress; 
    
    public event Action OnRefreshButtonPress; 
    
    public event Action<RoomItem> OnRoomButtonPress;

    public event Action OnCreateRoomOpenWindowButtonPress;
    
    public event Action<string> OnCreateRoomButtonPress;
    
    [SerializeField]
    private Button refreshButton;
    
    [SerializeField]
    private Button connectButton;
    
    [SerializeField]
    private Button createRoomButton;

    [SerializeField]
    private TextMeshProUGUI selectedRoomName;

    [SerializeField] 
    private RoomItemButton roomItemButtonPrefab;
    
    [SerializeField]
    private Transform content;
    
    [SerializeField] [Tooltip("Text input field for entering a room name")]
    private TMP_InputField roomName;

    [SerializeField] 
    private GameObject createRoomWindow;
    
    [SerializeField]
    private Button acceptRoomButton;
    
    [SerializeField]
    private TMP_Dropdown dropdown;
    
    
    private ObjectPool<RoomItemButton> _roomItemButtonPool;
    private List<RoomItemButton> _roomItemButtonsList;

    private const string RoomNamePrefix = "Room selected:{0}";

    private void Awake()
    {
        refreshButton.onClick.AddListener(RefreshButtonPress);
        connectButton.onClick.AddListener(ConnectButtonPress);
        acceptRoomButton.onClick.AddListener(CreateRoomButtonPress);
        createRoomButton.onClick.AddListener(CreateRoomOpenWindowButtonPress);
        

        _roomItemButtonPool = new ObjectPool<RoomItemButton>(this);
        _roomItemButtonPool.Configure(0,20);
        
        _roomItemButtonsList = new List<RoomItemButton>();
    }

    private void CreateRoomOpenWindowButtonPress()
    {
        OnCreateRoomOpenWindowButtonPress?.Invoke();
    }

    private void CreateRoomButtonPress()
    {
        OnCreateRoomButtonPress?.Invoke(roomName.text);
    }

    private void ConnectButtonPress()
    {
        OnConnectButtonPress?.Invoke();
    }

    private void RefreshButtonPress()
    {
        OnRefreshButtonPress?.Invoke();
    }

    public void SetRoomName(string newRoomName)
    {
        selectedRoomName.text=string.Format(RoomNamePrefix,newRoomName);
    }

    public void UpdateListOfRooms(List<RoomItem> rooms)
    {
        ClearRoomsButtons();
        
        if (rooms is null) return;
       
        foreach (RoomItem item in rooms)
        {   
            RoomItemButton newItemButton=_roomItemButtonPool.Get();
            _roomItemButtonsList.Add(newItemButton);
            newItemButton.SetRoom(item);
            newItemButton.Button.onClick.AddListener(()=>{OnRoomButtonPress?.Invoke(item);});
        }
        
    }

    private void ClearRoomsButtons()
    {
        foreach (RoomItemButton auxiliarItem in _roomItemButtonsList)
        {
            _roomItemButtonPool.Release(auxiliarItem);
        }
        _roomItemButtonsList.Clear();
    }

    private void ShowCreateRoomWindow()
    {
        createRoomWindow.SetActive(true);
    }

    public override void Show()
    {
        base.Show();
        createRoomWindow.SetActive(false);
    }

    public void OpenCreateRoomWindow()
    {
        ShowCreateRoomWindow();
    }

    public TypeOfRoom GetTypeOfRoomSelected()
    {
        int valueSelected = dropdown.value;
        return (TypeOfRoom)valueSelected;
    }

    #region IFactory<RoomItemButton>

    public RoomItemButton Create()
    {
        return Instantiate(roomItemButtonPrefab, Vector3.zero, Quaternion.identity, content);
    }

    #endregion
   
}