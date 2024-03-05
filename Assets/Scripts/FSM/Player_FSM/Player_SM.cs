using System.Collections.Generic;
using FSM;

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
