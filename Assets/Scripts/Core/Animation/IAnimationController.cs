using UnityEngine.Events;

namespace Archero.Animation
{
    public interface IAnimationController
    {
        event UnityAction OnEnd;
        void Play();
    }
}
