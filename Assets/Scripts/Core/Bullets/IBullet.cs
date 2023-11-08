using UnityEngine;

namespace Archero.Bullets
{
    public interface IBullet : IHideable, IActivatable
    {
        void SetPosition(Transform root);
        void SetRotation(Transform root);
        void SetFollowTarget (Transform target);
        void SetBehaviour(BulletType type);
    }
}
