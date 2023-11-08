using Archero.SO;
using UnityEngine;

namespace Archero.Enemies
{
    public interface IEnemy : IMovable, IRotatable, IHitable, IShootable, IHideable, ILocatable, ITransformable, IActivatable
    {
        EnemyData Stats { get; }
        Vector3 PositionTarget { get; }

        void BeginMove();
        void BeginToShoot();
    }
}
