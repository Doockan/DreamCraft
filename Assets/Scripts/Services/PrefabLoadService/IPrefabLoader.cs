using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.PrefabLoadService
{
    public interface IPrefabLoader : IService
    {
        Task<GameObject> LoadPrefab(string name);
    }
}