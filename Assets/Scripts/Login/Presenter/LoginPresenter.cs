using Cysharp.Threading.Tasks;
using Photon.Pun;
using Zenject;

public class LoginPresenter
{
    private readonly LoginView _loginView;
    private readonly Navigator _navigator;
    private readonly IRoomService _roomService;

    public LoginPresenter(LoginView loginView, Navigator navigator,IRoomService roomService)
    {
        _loginView = loginView;
        _navigator = navigator;
        _roomService = roomService;


        _loginView.OnConnectButtonClicked += ConnectHandler;
        _loginView.DisableConnectButton();
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
                _loginView.EnableConnectButton();
                break;
        }
    }
   
}