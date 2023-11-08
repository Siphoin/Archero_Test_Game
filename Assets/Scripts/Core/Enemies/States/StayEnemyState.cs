using Archero.States;
using UnityEngine;

namespace Archero.Enemies.States
{
    public class StayEnemyState : OwneringState<IEnemy>, IUpdatableState
    {
        public override void Enter()
        {
            Debug.Log("enemy start waiting start game");
        }

        public override void Exit()
        {
            Debug.Log("enemy end waiting start game");
        }

        public void Update()
        {
            Vector3 ownerPosition = new Vector3(Owner.Position.x, 0, Owner.Position.z);
            Vector3 targetPosition = new Vector3(Owner.PositionTarget.x, 0, Owner.PositionTarget.z);

            float distanceToMove = Vector3.Distance(ownerPosition, targetPosition);

            if (distanceToMove <= Owner.Stats.DistanceToStartChase)
            {
                Owner.Activate();
                Owner.BeginMove();
            }
        }
    }
}
