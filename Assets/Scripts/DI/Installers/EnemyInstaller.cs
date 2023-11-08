using Archero.Enemies;
using UnityEngine;
using Zenject;
using Archero.Exceptions;
using Archero.Extensions;
using System.Linq;

namespace Archero.DI.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyBase[] _enemies = new EnemyBase[0];

        private ISpawnPoint[] _spawnPoints;

        [SerializeField, Min(1)] private int _spawnCount = 4;

        private Transform _container;

        public override void InstallBindings()
        {
            if (_enemies.Length == 0)
            {
                throw new ArrayIsEmptyException("enemies array is empty");
            }


            _spawnPoints = FindObjectsOfType<SpawnPoint>().Where(x => x.Type == SpawnPointType.Enemy).ToArray();

            if (_spawnPoints.Length == 0)
            {
                throw new ArrayIsEmptyException("spawn points array is empty");
            }

            _container = new GameObject("Enemies").transform;

            for (int i = 0; i < _spawnCount; i++)
            {
                Spawn();
            }
        }

        private void Spawn ()
        {
            EnemyBase enemyType = _enemies.GetRandomElement();

            IEnemy newEnemy = Container.InstantiatePrefabForComponent<IEnemy>(enemyType);

            newEnemy.Transform.position = GetRandomPoint();

            newEnemy.Transform.SetParent(_container);
        }

        private Vector3 GetRandomPoint ()
        {
            ILocatable point = _spawnPoints.GetRandomElement();

            Vector3 randomPoint = point.Position + Random.insideUnitSphere * 2;

            return randomPoint;
        }
    }
}