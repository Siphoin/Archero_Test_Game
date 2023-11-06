using UnityEngine;
using Zenject;
using System;
using Archero.SO;

namespace Archero.DI.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _prefab;
        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private WeaponData _weapon;

        public override void InstallBindings()
        {
            if (!_prefab)
            {
                throw new NullReferenceException("prefab player is null");
            }

            if (!_spawnPoint)
            {
                throw new NullReferenceException("spawn point not seted for player installer");
            }

            var player = Container.InstantiatePrefabForComponent<Player>(_prefab, _spawnPoint.GetPosition(), Quaternion.identity, null);
            player.SetWeaponData(_weapon);
            Container.Bind<IPlayer>().To<Player>().FromInstance(player);
        }
    }
}