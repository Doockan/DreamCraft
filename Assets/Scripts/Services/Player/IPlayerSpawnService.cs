using System;
using Assets.Scripts.Player;
using Assets.Scripts.Services;

public interface IPlayerSpawnService : IService
{
    event Action<PlayerView> PlayerSpawned;
    public void Spawn();
}