using SiphoinUnityHelpers.Polling;
using UnityEngine;
using System;
using Archero.SO;

namespace Archero.Bullets
{
    public class BulletPool
    {
        private PoolMono<Bullet> _container;

        private readonly int _countOnStart = 15;

        public BulletPool (Transform root, WeaponData weapon)
        {
            if (!root)
            {
                throw new ArgumentNullException("root is null");
            }

            if (!weapon)
            {
                throw new ArgumentNullException("weapon data is null");
            }

            Transform rootContainer = new GameObject($"{root.name}_{nameof(BulletPool)}").transform;
            var prefab = weapon.PrefabBullet;
            _container = new PoolMono<Bullet>(prefab, rootContainer, _countOnStart, true);
        }

        public IBullet GetFreeBullet ()
        {
            return _container.GetFreeElement();
        }
    }
}
