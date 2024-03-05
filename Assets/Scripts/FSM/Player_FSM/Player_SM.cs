using System.Collections.Generic;
using FSM;
using UnityEngine;

public class Player_SM: StateMachine, ICharacterStateMachine
{
    private Character _character;
    
    private StateMachine _sm;
    private Dictionary<StateType, State> _states = new Dictionary<StateType, State>();
  
    
    public Player_SM(Character character)
    {
        _character = character;
    }
    
    public void InitBehaviour()
    {
        _sm = new StateMachine();
        AddState(StateType.Idle, new Idle_State());
        AddState(StateType.Move, new Move_State(_character.CommandManager));
        _sm.Initialize(_states[StateType.Move]);
        Subscribe();
     
    }
    
    public void UpdateBehaviour()
    {
        _sm.CurrentState.Update();
    }

    public void ChancgeState(StateType stateType)
    {
        _sm.ChangeState(_states[stateType]);
    }
    
    private void Subscribe()
    {
        CharacterFSMObserver.current.AddObserver(this);
    }

    private void UnSubscribe()
    {
        CharacterFSMObserver.current.RemoveObserver(this);
    }

    private void AddState(StateType stateType, State state)
    {
        _states[stateType] = state;
    }
}
