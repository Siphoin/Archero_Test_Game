using Archero.States;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Archero.Enemies.States
{
    public class ShootEnemyState : OwneringState<IEnemy>, IUpdatableState, IFixedUpdatableState
    {
        private CancellationTokenSource _cancelToken;

        public override void Enter()
        {
            _cancelToken = new CancellationTokenSource();

            Shooting().Forget();
        }

        public override void Exit()
        {
            _cancelToken.Cancel();
        }

        public void FixedUpdate()
        {
            Owner.Rotate();
        }

        public void Update()
        {
            Vector3 ownerPosition = new Vector3(Owner.Position.x, 0, Owner.Position.z);
            Vector3 targetPosition = new Vector3(Owner.PositionTarget.x, 0, Owner.PositionTarget.z);

            float distanceToShoot = Vector3.Distance(ownerPosition, targetPosition);

            if (distanceToShoot >= Owner.Stats.DistanceMove)
            {
                Owner.BeginMove();
            }
        }

        private async UniTask Shooting ()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Owner.Stats.Weapon.DelayShoot);

            while (true)
            {
                await UniTask.Delay(timeSpan, cancellationToken: _cancelToken.Token);

                Owner.Shoot();
            }
        }

        
    }
}
