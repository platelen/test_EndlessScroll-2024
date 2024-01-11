using System;
using System.Collections.Generic;

namespace FSM
{
    public class Fsm
    {
        private FsmState StateCurrent { get; set; }
        private Dictionary<Type, FsmState> _fsmStates = new Dictionary<Type, FsmState>();

        public void AddState(FsmState state)
        {
            _fsmStates.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : FsmState
        {
            var type = typeof(T);

            if (StateCurrent.GetType() == type)
            {
                return;
            }

            if (_fsmStates.TryGetValue(type, out var newState))
            {
                StateCurrent?.Exit();
                StateCurrent = newState;
                StateCurrent.Enter();
            }
        }

        public void Update()
        {
            StateCurrent?.Update();
        }
    }
}