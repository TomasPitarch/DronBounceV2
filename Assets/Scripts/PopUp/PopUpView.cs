using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpView : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI popUpText;
    
    [SerializeField] 
    private TextMeshProUGUI buttonText;

    [SerializeField] 
    private Button button;

    public void SetButtonText(string text)
    {
        buttonText.text = text;
    }

    public void SetPopUpText(string text)
    {
        popUpText.text = text;
    }

    public void SetAction(Action action)
    {
        if(action is null) return;
        button.onClick.AddListener(action.Invoke);
    }

    public void ShowPopUp()
    {
        button.onClick.AddListener(HidePopUp);
        gameObject.SetActive(true);
    }

    public void HidePopUp()
    {
        gameObject.SetActive(false);
        button.onClick.RemoveAllListeners();
    }
}
