using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "BotData", menuName = "StaticData/BotData")]
    public class BotData : ScriptableObject
    {
        [SerializeField] private string _botPrefabName = "Bot";
        [SerializeField] private float _spawnInterval = 3f;
        [SerializeField] private float _spawnRadius = 20f;
        [SerializeField] private float _mMinDistanceFromCamera = 10f;
        [SerializeField] private int _maxBots = 10;
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _damage = 10f;

        public string BotPrefabName => _botPrefabName;
        public float SpawnInterval => _spawnInterval;
        public float SpawnRadius => _spawnRadius;
        public float MinDistanceFromCamera => _mMinDistanceFromCamera;
        public int MaxBots => _maxBots;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float Damage => _damage;
    }
}