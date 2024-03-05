using System.Collections.Generic;
using FSM;
using UnityEngine;

public class Player_SM: StateMachine, ICharacterStateMachine
{
    private Character _character;
    
    private StateMachine _sm;
    private Dictionary<CharacterStateType, State> _states = new Dictionary<CharacterStateType, State>();
  
    
    public Player_SM(Character character)
    {
        _character = character;
    }
    
    public void InitBehaviour()
    {
        _sm = new StateMachine();
        AddState(CharacterStateType.Idle, new Idle_State());
        AddState(CharacterStateType.Move, new Move_State(_character.CommandManager));
        AddState(CharacterStateType.Fear, new Fear_State(_character));
        AddState(CharacterStateType.Death, new Death_State(_character));
        _sm.Initialize(_states[CharacterStateType.Move]);
        Subscribe();
     
    }
    
    public void UpdateBehaviour()
    {
        _sm.CurrentState.Update();
    }

    public void ChancgeState(CharacterStateType characterStateType)
    {
        _sm.ChangeState(_states[characterStateType]);
    }

    public bool StateCondition(CharacterStateType characterStateType)
    {
        return _sm.CurrentState == _states[characterStateType];
    }

    private void AddState(CharacterStateType characterStateType, State state)
    {
        _states[characterStateType] = state;
    }
    
    // TODO REFACTOR
    private void Subscribe()
    {
        CharacterFSMObserver.current.AddObserver(this);
    }
    // TODO REFACTOR
    private void UnSubscribe()
    {
        CharacterFSMObserver.current.RemoveObserver(this);
    }


}
