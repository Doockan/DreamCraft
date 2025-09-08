using System.Threading.Tasks;
using Assets.Scripts.Services;

namespace Assets.Scripts.Data
{
    public interface IStaticDataService : IService
    {
        Task LoadData();
        BotData Bot();
    }
}