using System;
using Assets.Scripts.Player;

namespace Assets.Scripts.Services.Player
{
    public interface IPlayerHandler : IService
    {
        event Action OnSpawn;
        PlayerView Player { get; }
    }
}