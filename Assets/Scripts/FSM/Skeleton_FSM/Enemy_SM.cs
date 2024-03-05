using FSM;
using UnityEngine;

public class Enemy_SM : StateMachine, ICharacterStateMachine
{
    private Enemy _enemy;
    
    private StateMachine _sm;

    private Follow_State _followState;

    public Enemy_SM(Transform transform)
    {
        _enemy = transform.GetComponent<Enemy>();
    }
    
    public void InitBehaviour()
    {
        _sm = new StateMachine();
        //_followState = new Follow_State(_enemy.Character.MovementType);
        _sm.Initialize(_followState);
    }

    public void UpdateBehaviour()
    {
        _sm.CurrentState.Update();
    }

    public void ChancgeState(StateType stateType)
    {
        throw new System.NotImplementedException();
    }
}
