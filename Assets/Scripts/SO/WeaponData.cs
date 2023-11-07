using Archero.Bullets;
using UnityEngine;

namespace Archero.SO
{
    [CreateAssetMenu]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private Bullet _prefabBullet;
        [SerializeField, Min(0.1f)] private float _delayShoot = 0.3f;

        public Bullet PrefabBullet => _prefabBullet;
        public float DelayShoot => _delayShoot;
    }
}