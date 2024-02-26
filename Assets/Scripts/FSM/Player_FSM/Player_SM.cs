using FSM;
using UnityEngine;

public class Player_SM: StateMachine, IStateMachine
{
    private Player _player;
    
    private StateMachine _sm;

    private Idle_State _idleState;
    private Move_State _moveState;

    public Player_SM(Transform transform)
    {
        _player = transform.GetComponent<Player>();
       
    }
    
    public void InitBehaviour()
    {
        _sm = new StateMachine();
        _idleState = new Idle_State();
       _moveState = new Move_State(_player.Character.MovementType);
        _sm.Initialize(_moveState);
    }

    public void UpdateBehaviour()
    {
        _sm.CurrentState.Update();
    }
}
