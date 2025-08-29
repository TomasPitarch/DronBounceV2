using System;

public class PopUpButtonModel
{
    public string PopUpText;
    public string ButtonText;
    public Action OnClick;

    public PopUpButtonModel(string popUpText, string buttonText, Action onClick)
    {
        PopUpText = popUpText;
        ButtonText = buttonText;
        OnClick = onClick;
    }
    public PopUpButtonModel(string popUpText, string buttonText)
    {
        PopUpText = popUpText;
        ButtonText = buttonText;
    }
}