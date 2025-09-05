using Assets.Scripts.Services.LoadSceneServices;

public class LoadGameSceneService : ILoadGameSceneService
{
    private const string GAME_SCENE = "MainScene";
    private readonly SceneLoader _sceneLoader;
    private readonly IPlayerSpawnService _playerSpawnService;

    public LoadGameSceneService(SceneLoader sceneLoader, IPlayerSpawnService playerSpawnService)
    {
        _sceneLoader = sceneLoader;
        _playerSpawnService = playerSpawnService;
    }

    public void LoadLevel()
    {
        _sceneLoader.Load(GAME_SCENE, InitializeGame);
    }

    private void InitializeGame()
    {
        _playerSpawnService.Spawn();
    }
}
