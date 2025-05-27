using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PingUI : MonoBehaviour
{
    private const string PingText = "Ping: {0}ms";

    [SerializeField] 
    private TextMeshProUGUI pingText;

    private INetworkService _networkService;
   
    [Inject]
    public void InjectDependencies(INetworkService networkService)
    {
        _networkService = networkService;
        _networkService.Ping.OnValueChanged += PingUpdate;
    }

    private void PingUpdate(int ping)
    {
        pingText.text = string.Format(PingText,ping);
    }
}
