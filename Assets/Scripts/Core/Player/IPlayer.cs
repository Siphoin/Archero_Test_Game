using Archero.SO;

namespace Archero
{
    public interface IPlayer : IMovable, IRotatable, IHitable, IShootable
    {
        void StopMove();
        void SetWeaponData(WeaponData weapon);
    }
}
