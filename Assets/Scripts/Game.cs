using System.Threading.Tasks;
using Assets.Scripts.Services;
using Assets.Scripts.Services.Player;
using Assets.Scripts.Services.SimpleBot;
using Assets.Scripts.Services.InputService;
using Assets.Scripts.Services.LoadSceneServices;
using Assets.Scripts.Services.PrefabLoadService;

namespace Assets.Scripts
{
    public class Game
    {
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public Game(SceneLoader sceneLoader, AllServices services)
        {
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices().ConfigureAwait(false);
        }

        public void Update(float deltaTime)
        {
            _services.TickAll(deltaTime);
        }

        private async Task RegisterServices()
        {
            _services.RegisterSingle<IPrefabLoader>(new PrefabLoaded());

            _services.RegisterSingle<IPlayerSpawnService>(new PlayerSpawnService(
                _services.Single<IPrefabLoader>()
            ));

            _services.RegisterSingle<ILoadGameSceneService>(new LoadGameSceneService(_sceneLoader,
                _services.Single<IPlayerSpawnService>()
            ));

            _services.RegisterSingle<IPlayerHandler>(new PlayerHandler(
                _services.Single<IPlayerSpawnService>()
            ));

            _services.RegisterSingle<IPlayerDieHandler>(new PlayerDieHandler(
                _services.Single<IPlayerHandler>()
            ));

            _services.RegisterSingle<IInputHandler>(new InputHandler(
                _services.Single<IPlayerHandler>()
            ));

            _services.RegisterSingle<IBotSpawner>(new BotSpawner(
                _services.Single<IPrefabLoader>(),
                _services.Single<IPlayerHandler>(),
                "Bot",
                spawnInterval: 3f,
                spawnRadius: 20f,
                minDistanceFromCamera: 10f,
                maxBots: 10
            ));


            await _services.Single<IBotSpawner>().Initialize();

            _services.Single<ILoadGameSceneService>().LoadLevel();
        }
    }
}
