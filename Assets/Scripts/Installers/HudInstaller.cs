using UnityEngine;
using Zenject;

public class HudInstaller : MonoInstaller
{
    [SerializeField]
    private HUDView hudView;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ScoreManager scoreManager;
    public override void InstallBindings()
    {
        Container.Bind<HUDView>().FromInstance(hudView).AsSingle();
        Container.Bind<HudPresenter>().AsSingle();
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<ScoreManager>().FromInstance(scoreManager).AsSingle();
    
    }
}
