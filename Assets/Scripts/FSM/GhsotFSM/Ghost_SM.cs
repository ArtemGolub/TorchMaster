using System.Collections.Generic;
using FSM;
using State = FSM.State;
using StateMachine = FSM.StateMachine;

public class Ghost_SM: StateMachine, ICharacterStateMachine
{
    private Character _character;
    
    private StateMachine _sm;
    private Dictionary<CharacterStateType, State> _states = new Dictionary<CharacterStateType, State>();
    
    public Ghost_SM(Character character)
    {
        _character = character;
    }
    
    public void InitBehaviour()
    {
        _sm = new StateMachine();
        
        AddState(CharacterStateType.Idle, new Idle_State(_character));
        
        AddState(CharacterStateType.Move, new Move_State(_character,_character.CommandManager));
        AddState(CharacterStateType.Follow, new Follow_State(_character, _character.CommandManager));
        
        AddState(CharacterStateType.Attack, new AttackState(_character));
        
        AddState(CharacterStateType.Death, new GhostDeath_State(_character));
        
        _sm.Initialize(_states[CharacterStateType.Move]);
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
}