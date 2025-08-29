using UnityEngine;

public class PopUpManager : MonoBehaviour, IPopUpManager
{
    [SerializeField] private PopUpView popUpView;

    public void ShowPopUp(PopUpButtonModel model)
    {
        popUpView.SetAction(model?.OnClick);
        popUpView.SetButtonText(model?.ButtonText);
        popUpView.SetPopUpText(model?.PopUpText);
        popUpView.ShowPopUp();
    }
}