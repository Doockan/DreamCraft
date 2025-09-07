using UnityEngine;
using System.Threading.Tasks;

namespace Assets.Scripts.Services.SimpleBot
{
    public interface IBotSpawner : IService
    {
        Task Initialize();
        void Tick(float deltaTime);
        void DespawnAll();
        int ActiveBotCount { get; }
        Transform BotsParent { get; }
    }
}