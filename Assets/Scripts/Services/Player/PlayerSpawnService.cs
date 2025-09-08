using System;
using UnityEngine;
using Assets.Scripts.Player;
using Assets.Scripts.Services.PrefabLoadService;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Services.Player
{
    public class PlayerSpawnService : IPlayerSpawnService
    {
        private readonly IPrefabLoader _prefabLoader;
        private PlayerView _playerView;

        public event Action<PlayerView> PlayerSpawned;

        public PlayerSpawnService(IPrefabLoader prefabLoader)
        {
            _prefabLoader = prefabLoader;
        }

        public async void Spawn()
        {
            var playerPrefab = await _prefabLoader.LoadPrefab("Player");

            var playerObject = Object.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

            _playerView = playerObject.GetComponent<PlayerView>();
            PlayerSpawned?.Invoke(_playerView);
        }
    }
}