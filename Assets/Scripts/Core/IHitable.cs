using System;
using UnityEngine.Events;

namespace Archero
{
    public interface IHitable
    {
        event UnityAction<int> OnHit;
        event EventHandler OnDealth;
        int Health { get; }
        bool IsDied { get; }
        void Hit(int value);
    }
}
