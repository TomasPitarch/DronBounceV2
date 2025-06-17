using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RoomItemButton : MonoBehaviour,IPoolable
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    
    private RoomItem _roomItem;
    public Button Button=>button;
    public RoomItem RoomItem=>_roomItem;

    public void SetRoom(RoomItem roomItem)
    {
        gameObject.SetActive(true);
        _roomItem = roomItem;
        text.text=string.Format("Room:{0} ({1})      {2}/{1} ",_roomItem.RoomName,_roomItem.NumberOfPlayers()   ,_roomItem.ActualPlayers);
    }
    
    #region IPoolable
    public void OnRelease()
    {
        button.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }
    #endregion
    
}