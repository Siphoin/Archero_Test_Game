using Archero.States;
using UnityEngine;

namespace Archero.Enemies.States
{
    public class MoveEnemyState : OwneringState<IEnemy>, IUpdatableState
    {
        public override void Enter()
        {
            Debug.Log("enemy begin move to player");
        }

        public override void Exit()
        {
            Debug.Log("enemy ending move");
        }

        public void Update()
        {
            Vector3 ownerPosition = new Vector3(Owner.Position.x, 0, Owner.Position.z);
            Vector3 targetPosition = new Vector3(Owner.PositionTarget.x, 0, Owner.PositionTarget.z);

            float distanceToShoot = Vector3.Distance(ownerPosition, targetPosition);

            if (distanceToShoot <= Owner.Stats.DistanceMove)
            {
                Owner.BeginToShoot();
            }

            Owner.Move();
            Owner.Rotate();
        }
    }
}
