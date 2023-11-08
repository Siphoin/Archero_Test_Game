using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Archero.Animation
{
    public abstract class AnimationComponent : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _duration = 0.3f;
        [SerializeField] private Ease _ease = Ease.Linear;
        public event UnityAction OnEnd;

        protected float Duration => _duration;
        protected Ease Ease => _ease;

        protected virtual void OnComplete()
        {
            OnEnd?.Invoke();
        }

        public abstract Tween Play();
        public abstract bool IsPlayingOnStart();
    }
}
