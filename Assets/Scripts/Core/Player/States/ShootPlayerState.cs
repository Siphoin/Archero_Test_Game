using Archero.Bullets;
using Archero.Enemies;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Archero.States
{
    public class ShootPlayerState : OwneringState<IPlayer>, IFixedUpdatableState
    {
        private List<IEnemy> _targets;

        private CancellationTokenSource _cancelToken;

        private IEnemy CurrentTarget
        {
            get
            {
                return _targets.OrderBy(t => Vector3.Distance(Owner.Position, t.Position)).FirstOrDefault(x => !x.IsDied && x.IsActive);
            }
        }

        public ShootPlayerState ()
        {
            _targets = new List<IEnemy>();
        }

        public override void Enter()
        {
            _cancelToken = new CancellationTokenSource();

            Shooting().Forget();

            Debug.Log("player begin shoot");
        }

        public override void Exit()
        {
            Debug.Log("player end shoot");

            _targets.Clear();

            _cancelToken.Cancel();
        }

        public void FixedUpdate()
        {
                var cast = Physics.SphereCastAll(Owner.Position, Owner.Weapon.Radius, Owner.Forward);

                foreach (var item in cast)
                {
                    GameObject gameObject = item.transform.gameObject;

                    if (gameObject.TryGetComponent(out IEnemy enemy))
                    {
                        bool contains = _targets.Contains(enemy);

                        if (!contains && !enemy.IsDied && enemy.IsActive)
                        {
                            enemy.OnDeath += OnDeathEnemy;
                            _targets.Add(enemy);
                        }

                    }
                }

                if (CurrentTarget != null)
                {
                Owner.Rotate(CurrentTarget.Position);
                }
                


        }

        private void OnDeathEnemy(object sender, EventArgs e)
        {
            IEnemy enemy = sender as IEnemy;

            enemy.OnDeath -= OnDeathEnemy;

            _targets.Remove(enemy);
        }

        private async UniTask Shooting ()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Owner.Weapon.DelayShoot);

            while (true)
            {

                await UniTask.Delay(timeSpan, cancellationToken: _cancelToken.Token);

                BulletType behaviourBullet = CurrentTarget is FlyEnemy ? BulletType.ToTarget : BulletType.Ground;

                Owner.SetBehaviourShoot(behaviourBullet);

                if (behaviourBullet == BulletType.ToTarget)
                {
                    Owner.SetTargetForBullets(CurrentTarget.Transform);
                }


                await UniTask.WaitUntil(() => CurrentTarget != null, cancellationToken: _cancelToken.Token);

                Owner.Shoot();

            }
        }
    }
}
