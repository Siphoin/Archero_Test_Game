using System;
using System.Collections.Generic;
using System.Linq;
namespace Archero.States
{
    public partial class StateMachine
    {
        private Dictionary<Type, IState> _states;

        public IState Current { get; private set; }

        public IState DefaultState => _states.FirstOrDefault().Value;

        public void Initialize(IEnumerable<IState> states)
        {
            if (_states != null)
            {
                throw new InvalidOperationException("states of state machine are initialized");
            }

            _states = new Dictionary<Type, IState>();

            _states = states.ToDictionary(state => state.GetType());

            SetStateByDefault();


        }

        public void SetState<T>() where T : IState
        {
            Type stateType = typeof(T);

            SetState(stateType);
        }

        private void SetState(Type stateType)
        {
            if (!_states.ContainsKey(stateType))
            {
                throw new KeyNotFoundException($"state with key {stateType.Name} not found");
            }

            Current?.Exit();
            Current = _states[stateType];
            Current.Enter();
        }

        public void SetStateByDefault ()
        {
            SetState(DefaultState.GetType());
        }

        public void StopState()
        {
            Current?.Exit();
            Current = null;
        }
        

        public void Update()
        {
            if (Current is null)
            {
                return;
            }

            if (Current is IUpdatableState updateState)
            {
                updateState.Update();
            }
        }

        public void FixedUpdate()
        {
            if (Current is null)
            {
                return;
            }

            if (Current is IFixedUpdatableState fixedUpdateState)
            {
                fixedUpdateState.FixedUpdate();
            }
        }
    }
}
