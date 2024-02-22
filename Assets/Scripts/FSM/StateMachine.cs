namespace FSM
{
    public class StateMachine
    {
        public State CurrentState { get; set; }
        public void Initialize(State startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void UpdateState()
        {
            CurrentState.Update();
        }
        public void ChangeState(State newState)
        {
            if (CurrentState == newState) return;
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}