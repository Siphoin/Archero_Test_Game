using UnityEngine;

namespace Archero.Bullets
{
    public interface IBullet
    {
        void SetPosition(Transform root);
        void SetRotation(Transform root);
    }
}
