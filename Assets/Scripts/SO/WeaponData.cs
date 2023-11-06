using Archero.Bullets;
using UnityEngine;

namespace Archero.SO
{
    [CreateAssetMenu]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private Bullet _prefabBullet;
        [SerializeField, Min(1)] private int _damage = 1;

        public Bullet PrefabBullet { get => _prefabBullet; set => _prefabBullet = value; }
        public int Damage => _damage;
    }
}