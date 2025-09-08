using System;
using UnityEditor;

namespace Assets.Scripts.Services.Player
{
    public class PlayerDieHandler : IPlayerDieHandler, IDisposable
    {
        private readonly IPlayerHandler _playerHandler;

        public PlayerDieHandler(IPlayerHandler playerHandler)
        {
            _playerHandler = playerHandler;
            _playerHandler.OnSpawn += Initialize;
        }

        private void Initialize()
        {
            _playerHandler.Player.Health.OnDie += Die;
        }

        private void Die()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }

        public void Dispose()
        {
            _playerHandler.Player.Health.OnDie -= Die;
        }
    }
}
