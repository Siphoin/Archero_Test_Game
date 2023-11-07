using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Archero.States
{
    public partial class StateMachine
    {
        private Dictionary<Type, IState> _states;

        private IState _current;

        private Type DefaultState => _states.FirstOrDefault().Value.GetType();

        public void Initialize(IEnumerable<IState> states)
        {
            if (_states != null)
            {
                throw new InvalidOperationException("states of state machine are initialized");
            }

            _states = new Dictionary<Type, IState>();

            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
            }

            SetStateByDefault();


        }

        public void SetState<T>() where T : IState
        {
            Type stateType = typeof(T);

            SetState(stateType);
        }

        public void AddState<T>(IState state) where T : IState
        {
            Type stateType = typeof(T);

            if (state is null)
            {
                throw new ArgumentNullException("state argument is null");
            }

            if (_states.ContainsKey(stateType))
            {
                throw new InvalidOperationException($"key of state {stateType} already exits of state machine");
            }

            _states.Add(stateType, state);
        }

        private void SetState(Type stateType)
        {
            if (!_states.ContainsKey(stateType))
            {
                throw new KeyNotFoundException($"state with key {stateType.Name} not found");
            }

            _current?.Exit();
            _current = _states[stateType];
            _current.Enter();
        }

        public void SetStateByDefault ()
        {
            SetState(DefaultState);
        }

        public void StopState()
        {
            _current?.Exit();
            _current = null;
        }
        

        public void Update()
        {
            if (_current is null)
            {
                return;
            }

            if (_current is IUpdatableState)
            {
                var updateState = _current as IUpdatableState;
                updateState.Update();
            }
        }

        public void FixedUpdate()
        {
            if (_current is null)
            {
                return;
            }

            if (_current is IFixedUpdatableState)
            {
                var fixedUpdateState = _current as IFixedUpdatableState;
                fixedUpdateState.FixedUpdate();
            }
        }
    }
}
