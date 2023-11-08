using DG.Tweening;
using UnityEngine;

namespace Archero.Animation
{
    public class AnimationShow : AnimationComponent
    {
        private Vector3 _scaleOnStart;

        private void Start()
        {
            _scaleOnStart = transform.localScale;

            Play();
        }
        public override Tween Play()
        {
            transform.localScale = Vector3.zero;
            return transform.DOScale(_scaleOnStart, Duration).SetEase(Ease).OnComplete(OnComplete);
        }

        public override bool IsPlayingOnStart()
        {
            return true;
        }
    }
}
