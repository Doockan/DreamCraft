using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Data
{
    public class StaticDataService : IStaticDataService
    {
        private BotData _botData;

        public async Task LoadData()
        {
            var addressable = Addressables.LoadAssetAsync<BotData>(nameof(BotData));
            await addressable.Task;

            if (addressable.Status == AsyncOperationStatus.Succeeded)
            {
                _botData = addressable.Result;
            }
        }

        public BotData Bot() => _botData;
    }
}