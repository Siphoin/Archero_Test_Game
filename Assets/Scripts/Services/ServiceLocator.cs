using System;
using System.Collections.Generic;

namespace Archero.Services
{
    public class ServiceLocator : IInitializable
    {
        private static Dictionary<Type, IService> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<Type, IService>();
        }

        public void Initialize()
        {
            IService[] services =
            {
                new TimeService(),
                new UIService(),
                new UIMenuService(),
                new LevelService(),
            };

            foreach (var service in services)
            {
                service.Initialize();

                _services.Add(service.GetType(), service);
            }


        }

        internal T Get<T>() where T : IService
        {
            return (T)_services[typeof(T)];
        }
    }
}
