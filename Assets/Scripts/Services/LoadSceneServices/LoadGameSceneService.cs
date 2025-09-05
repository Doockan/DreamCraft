using Assets.Scripts.Services.LoadSceneServices;

public class LoadGameSceneService : ILoadGameSceneService
{
    private const string GAME_SCENE = "MainScene";
    private readonly SceneLoader _sceneLoader;

    public LoadGameSceneService(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void LoadLevel()
    {
        _sceneLoader.Load(GAME_SCENE, InitializeGame);
    }

    private void InitializeGame()
    {
    }
}
