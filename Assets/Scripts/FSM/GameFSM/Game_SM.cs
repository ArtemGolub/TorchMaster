using System.Collections.Generic;
using FSM;

public class Game_SM : StateMachine
{
    private StateMachine _sm;
    private Dictionary<GameState, State> _states = new Dictionary<GameState, State>();
    
    public void InitBehaviour()
    {
        _sm = new StateMachine();
       AddState(GameState.Start, new StartState());
       AddState(GameState.Gameplay, new GameplayState());
       AddState(GameState.Pause, new PauseState());
        _sm.Initialize(_states[GameState.Start]);
    }

    public void UpdateBehaviour()
    {
        _sm.CurrentState.Update();
    }

    public void ChancgeState(GameState gameState)
    {
        _sm.ChangeState(_states[gameState]);
    }

    public bool StateCondition(GameState gameState)
    {
        return _sm.CurrentState == _states[gameState];
    }

    private void AddState(GameState gameState, State state)
    {
        _states[gameState] = state;
    }
}
