using UnityEngine;
using Assets.Scripts.Services;
using Assets.Scripts.Services.LoadSceneServices;

namespace Assets.Scripts
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(new SceneLoader(this), AllServices.Container);

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _game.Update(Time.deltaTime);
        }
    }
}