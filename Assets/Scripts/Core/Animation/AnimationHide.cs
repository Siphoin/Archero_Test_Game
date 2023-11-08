using DG.Tweening;
using UnityEngine;

namespace Archero.Animation
{
    public partial class AnimationHide : AnimationComponent
    {
        public override Tween Play()
        {
            return transform.DOScale(Vector3.zero, Duration).SetEase(Ease).OnComplete(OnComplete);
        }

        public override bool IsPlayingOnStart()
        {
            return false;
        }

    }
}