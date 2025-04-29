 using UnityEngine;
 using UnityEngine.UI;
 using UnityEngine.EventSystems;
 using System.Collections;
 using Zenject;


 public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler,
                                                 IPointerDownHandler,IPointerExitHandler,
                                                 IPointerUpHandler
{
    [SerializeField] string S_OnPointerClick;
    [SerializeField] string S_OnPointerDown;
    [SerializeField] string S_OnPointerEnter;
    [SerializeField] string S_OnPointerExit;
    [SerializeField] string S_OnPointerUp;
    
    private ISoundService _soundService;
    
    [Inject]
    public void Initialize(ISoundService soundService)
    {
        _soundService = soundService;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick");
        if (S_OnPointerClick != "")
        {
            _soundService.PlaySound(S_OnPointerClick);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("S_OnPointerDown");
        if (S_OnPointerDown != "")
        {
            _soundService.PlaySound(S_OnPointerDown);
        }
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        //Debug.Log("onpointereo");
        if(S_OnPointerEnter!= "")
        {
            _soundService.PlaySound(S_OnPointerEnter);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("S_OnPointerExit");
        if (S_OnPointerExit != "")
        {
            _soundService.PlaySound(S_OnPointerExit);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("S_OnPointerUp");
        if (S_OnPointerUp != "")
        {
            _soundService.PlaySound(S_OnPointerUp);
        }
    }

}

