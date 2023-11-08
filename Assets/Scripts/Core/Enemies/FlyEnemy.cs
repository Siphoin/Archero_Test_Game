using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Archero.Enemies
{
    public class FlyEnemy : EnemyBase
    {
        private float _startOffset;

        protected override void Start()
        {
            base.Start();

            _startOffset = OffsetAgent;
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject gameObject = collision.gameObject;

            if (OffsetAgent == _startOffset)
            {
                if (gameObject.TryGetComponent(out Wall wall))
                {
                    ChangeOffset(wall.Bounds.max.y * 1.5f, 1f).Forget();
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            GameObject gameObject = collision.gameObject;

            if (gameObject.TryGetComponent(out Wall _))
            {
                ChangeOffset(_startOffset, 1f).Forget();
            }
        }

        private async UniTask ChangeOffset(float targetOffset, float duration)
        {
            float startTime = Time.time;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime = Time.time - startTime;
                OffsetAgent = Mathf.Lerp(OffsetAgent, targetOffset, elapsedTime / duration);
                await UniTask.Yield();
            }

            OffsetAgent = targetOffset;
        }
    }
}