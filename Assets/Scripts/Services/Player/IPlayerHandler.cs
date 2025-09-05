using Assets.Scripts.Player;

namespace Assets.Scripts.Services.Player
{
    public interface IPlayerHandler : IService
    {
        PlayerView Player { get; }
    }
}