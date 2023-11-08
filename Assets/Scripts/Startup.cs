using Archero.Repositories;
using Archero.Services;
using UnityEngine;

namespace Archero
{
    public static class Startup
	{
        private static RepositoryDb _dbRepository;

        private static ServiceLocator _serviceLocator;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Main()
        {
            _dbRepository = new RepositoryDb();

            _dbRepository.Initialize();

            _serviceLocator = new ServiceLocator();

            _serviceLocator.Initialize();

        }

        public static T GetRepository<T>() where T : IRepository
        {
            return _dbRepository.Get<T>();
        }

        public static T GetService<T>() where T : IService
        {
            return _serviceLocator.Get<T>();
        }
    }
}