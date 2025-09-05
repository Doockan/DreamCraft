using UnityEngine;
using Assets.Scripts.Services.PrefabLoadService;

namespace Assets.Scripts.Services.Player
{
    public class PlayerSpawnService : IPlayerSpawnService
    {
        private readonly IPrefabLoader _prefabLoader;
        private GameObject _playerView;

        public PlayerSpawnService(IPrefabLoader prefabLoader)
        {
            _prefabLoader = prefabLoader;
        }

        public async void Spawn()
        {
            _playerView = await _prefabLoader.LoadPrefab("Player");

            GameObject.Instantiate(_playerView, Vector3.zero, Quaternion.identity);
        }
    }
}