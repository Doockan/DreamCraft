using System;
using Assets.Scripts.Player;

namespace Assets.Scripts.Services.Player
{
    public class PlayerHandler : IPlayerHandler, IDisposable
    {
        private readonly IPlayerSpawnService _playerSpawnService;
        public PlayerView Player { get; private set; }
        public event Action OnSpawn;

        public PlayerHandler(IPlayerSpawnService playerSpawnService)
        {
            _playerSpawnService = playerSpawnService;
            _playerSpawnService.PlayerSpawned += OnSpawned;
        }

        private void OnSpawned(PlayerView view)
        {
            Player = view;
            OnSpawn?.Invoke();
        }

        public void Dispose()
        {
            _playerSpawnService.PlayerSpawned -= OnSpawned;
        }
    }
}
