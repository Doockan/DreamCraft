using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Services.PrefabLoadService
{
    public class PrefabLoaded : IPrefabLoader
    {
        public async Task<GameObject> LoadPrefab(string name)
        {
            var addressable = Addressables.LoadAssetAsync<GameObject>(name);
            await addressable.Task;

            if (addressable.Status == AsyncOperationStatus.Succeeded)
            {
                return addressable.Result;
            }
            else
            {
                Debug.LogError($"Failed to load prefab '{name}' from Addressables.");
                return null;
            }
        }
    }
}