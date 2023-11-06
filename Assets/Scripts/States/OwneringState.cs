using System;

namespace Archero.States
{
    public abstract class OwneringState<T> : IState
    {
        protected T Owner { get; private set; }

        public abstract void Enter();

        public abstract void Exit();

        public virtual void SetOwner (T owner)
        {
            if (Owner is null)
            {
                Owner = owner;
            }

            else
            {
                throw new InvalidOperationException("owner not is null");
            }
        }
    }
}