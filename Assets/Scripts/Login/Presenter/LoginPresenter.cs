using Cysharp.Threading.Tasks;
using Photon.Pun;
using Zenject;

public class LoginPresenter
{
    private readonly LoginView _loginView;
    private readonly Navigator _navigator;
    private readonly IRoomService _roomService;
    private readonly IPopUpManager _popUpManager;

    private readonly PopUpButtonModel _nameErrorPopUpModel;
        


    public LoginPresenter(LoginView loginView, Navigator navigator,IRoomService roomService, IPopUpManager popUpManager)
    {
        _loginView = loginView;
        _navigator = navigator;
        _roomService = roomService;
        _popUpManager = popUpManager;


        _loginView.OnConnectButtonClicked += ConnectHandler;
        _loginView.DisableConnectButton();
        
         _nameErrorPopUpModel = new PopUpButtonModel("Error : Name already exists, please choose another one.", "Ok");
         _loginView.SetNickName(_roomService.GetLastNickName());
        
    }
  
    private void ConnectHandler()
    {
        _loginView.DisableConnectButton();
        OnAuthenticationNameResponse(_roomService.AuthenticateName(_loginView.GetNickName()));
    }

    private void OnAuthenticationNameResponse(AuthenticateResponse response)
    {
        switch (response)
        {
            case AuthenticateResponse.Accepted:
            _navigator.OpenScreen("Room");
            break;
            case AuthenticateResponse.NameAlreadyExists:
                _popUpManager.ShowPopUp(_nameErrorPopUpModel);
                _loginView.EnableConnectButton();
                break;
        }
    }
   
}