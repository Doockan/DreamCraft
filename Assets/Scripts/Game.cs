using Assets.Scripts.Services;
using Assets.Scripts.Services.InputService;
using Assets.Scripts.Services.LoadSceneServices;

public class Game
{
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public Game(SceneLoader sceneLoader, AllServices services)
    {
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();

        _services.Single<ILoadGameSceneService>().LoadLevel();
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<ILoadGameSceneService>(new LoadGameSceneService(_sceneLoader));
        _services.RegisterSingle<IInputHandler>(new InputHandler());
    }
}
