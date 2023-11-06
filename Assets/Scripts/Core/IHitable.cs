using UnityEngine.Events;

namespace Archero
{
    public interface IHitable
    {
        event UnityAction<int> OnHit;
        event UnityAction OnDealth;
        int Health { get; }
        void Hit(int value);
    }
}
