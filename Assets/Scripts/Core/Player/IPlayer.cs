using Archero.Bullets;
using Archero.SO;
using UnityEngine;

namespace Archero
{
    public interface IPlayer : IMovable, IRotatable, IHitable, IShootable, ILocatable, ITransformable
    {
        WeaponPlayerData Weapon {  get; }
        Vector3 Forward { get; }

        void StopMove();
        void SetWeaponData(WeaponPlayerData weapon);
        void Rotate(Vector3 position);
        void SetBehaviourShoot(BulletType behavipurBullet);
        void SetTargetForBullets(Transform transform);
    }
}
