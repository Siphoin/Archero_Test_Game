using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Archero.Animation
{
    public class AnimationController : MonoBehaviour, IAnimationController
    {
        private int _count;
        private int _countFinished;

        public event UnityAction OnEnd;

        private AnimationComponent[] _animationComponents;

        private void Start()
        {
            _animationComponents = GetComponents<AnimationComponent>().Where(x => !x.IsPlayingOnStart()).ToArray();

            _count = _animationComponents.Length;

            foreach (var animationComponent in _animationComponents)
            {
                animationComponent.OnEnd += OnEndAnimation;
            }
        }

        private void OnEndAnimation()
        {
            _countFinished++;

            if (_countFinished == _count)
            {
                foreach (var animationComponent in _animationComponents)
                {
                    animationComponent.OnEnd -= OnEndAnimation;
                }

                OnEnd?.Invoke();
            }
        }

        public void Play()
        {
            foreach (var animationComponent in _animationComponents)
            {
                animationComponent.Play();
            }
        }
    }
}