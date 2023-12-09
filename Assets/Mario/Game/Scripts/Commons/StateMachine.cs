using Mario.Game.Interfaces;
using System;

namespace Mario.Game.Commons
{
    public abstract class StateMachine
    {
        #region Objects
        private IState _previousState;
        private IState _nextState;
        #endregion

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

            _nextState = nextState;
            return true;
        }
        public bool TransitionToPreviousState() => TransitionTo(_previousState);
        public Type GetPreviousStateType() => _previousState.GetType();
        public void Update()
        {
            if (CurrentState != null && _nextState == null)
                CurrentState.Update();

            ChangeState();
        }
        #endregion

        #region Private Methods
        private void ChangeState()
        {
            if (_nextState != null)
            {
                CurrentState.Exit();
                _previousState = CurrentState;
                CurrentState = _nextState;
                _nextState.Enter();

                StateChanged?.Invoke(_nextState);
                _nextState = null;
            }
        }
        #endregion
    }
}