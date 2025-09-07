using System.Collections.Generic;

namespace Assets.Scripts.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();
        private readonly List<ITickable> _tickables = new List<ITickable>();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
            if (implementation is ITickable tickable)
                _tickables.Add(tickable);
        }

        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;


        public void TickAll(float deltaTime)
        {
            foreach (var tickable in _tickables)
                tickable.Tick(deltaTime);
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}