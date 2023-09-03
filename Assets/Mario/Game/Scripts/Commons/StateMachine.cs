using Mario.Game.Interfaces;
using System;

namespace Mario.Game.Commons
{
    public abstract class StateMachine
    {
        #region Properties
        protected IState CurrentState { get; private set; }
        #endregion

        #region Events
        public event Action<IState> StateChanged;
        #endregion

        #region Public Methods
        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();

            StateChanged?.Invoke(state);
        }
        public bool TransitionTo(IState nextState)
        {
            if (nextState == null)
                return false;

            if (nextState == CurrentState)
                return false;

            UnityEngine.Debug.Log(nextState);
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();

            StateChanged?.Invoke(nextState);
            return true;
        }
        public void Update()
        {
            if (CurrentState != null)
                CurrentState.Update();
        }
        #endregion
    }
}