using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using Assets.Scripts.Services.Player;
using Assets.Scripts.Services.PrefabLoadService;

namespace Assets.Scripts.Services.SimpleBot
{
    public class BotSpawner : IBotSpawner, ITickable
    {
        private readonly IPrefabLoader _prefabLoader;
        private readonly IPlayerHandler _playerHandler;
        private readonly Camera _camera;

        private readonly List<GameObject> _activeBots = new List<GameObject>();
        private readonly Transform _botsParent;

        private readonly string _botPrefabName;
        private readonly float _spawnInterval;
        private readonly float _spawnRadius;
        private readonly float _minDistanceFromCamera;
        private readonly int _maxBots;

        private GameObject _botPrefab;
        private float _timer;

        public BotSpawner(
            IPrefabLoader prefabLoader,
            IPlayerHandler playerHandler,
            string botPrefabName,
            float spawnInterval = 3f,
            float spawnRadius = 20f,
            float minDistanceFromCamera = 10f,
            int maxBots = 10)
        {
            _prefabLoader = prefabLoader;
            _playerHandler = playerHandler;
            _botPrefabName = botPrefabName;
            _spawnInterval = spawnInterval;
            _spawnRadius = spawnRadius;
            _minDistanceFromCamera = minDistanceFromCamera;
            _maxBots = maxBots;
        }

        public Transform BotsParent => _botsParent;
        public int ActiveBotCount => _activeBots.Count;

        public async Task Initialize()
        {
            _botPrefab = await _prefabLoader.LoadPrefab(_botPrefabName);
        }

        public void Tick(float deltaTime)
        {
            if (_botPrefab == null || _playerHandler.Player == null) return;

            _timer += deltaTime;
            if (_timer >= _spawnInterval)
            {
                _timer = 0f;
                if (_activeBots.Count < _maxBots)
                {
                    SpawnBot();
                }
            }
        }

        private void SpawnBot()
        {
            Vector3 spawnPos = GetSpawnPosition();
            GameObject botObj = Object.Instantiate(_botPrefab, spawnPos, Quaternion.identity);

            var botAI = botObj.GetComponent<SimpleBotAI>();
            if (botAI != null)
            {
                botAI.Initialize(_playerHandler.Player.transform);
            }

            _activeBots.Add(botObj);

            botObj.GetComponent<Health>().OnDie += () =>
            {
                _activeBots.Remove(botObj);
                botAI.Destroy();
            };
        }

        private Vector3 GetSpawnPosition()
        {
            Vector3 playerPos = _playerHandler.Player.transform.position;

            for (int i = 0; i < 10; i++)
            {
                Vector3 randomDir = Random.onUnitSphere;
                randomDir.y = 0f;
                Vector3 candidate = playerPos + randomDir * _spawnRadius;

                Vector3 viewportPos = _playerHandler.Player.Camera.WorldToViewportPoint(candidate);

                if ((viewportPos.x < 0 || viewportPos.x > 1 ||
                     viewportPos.y < 0 || viewportPos.y > 1) &&
                    viewportPos.z > _minDistanceFromCamera)
                {
                    return candidate;
                }
            }

            return playerPos - _playerHandler.Player.transform.forward * _spawnRadius;
        }

        public void DespawnAll()
        {
            foreach (var bot in _activeBots)
            {
                if (bot != null)
                    Object.Destroy(bot);
            }

            _activeBots.Clear();
        }
    }
}