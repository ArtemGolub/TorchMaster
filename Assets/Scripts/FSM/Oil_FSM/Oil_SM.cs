using FSM;
using UnityEngine;

public class Oil_SM : StateMachine, IItemStateMachine
    
{
    private Oil oil;
    private IInventory _inventory;

    private StateMachine _sm;
    private Placed_State _placedState;
    private Grabed_State _grabedState;
    private TargetSearch_State _targetSearch;
    private Throw_State _throwState;

    public Oil_SM(Transform transform)
    {
        oil = transform.GetComponent<Oil>();
    }
    public void InitBehaviour()
    {
        _sm = new StateMachine();
        
        _placedState = new Placed_State();
        _grabedState = new Grabed_State(oil.item);
        _targetSearch = new TargetSearch_State();
        _throwState = new Throw_State();
        
        _sm.Initialize(_placedState);
    }

    public void UpdateBehaviour()
    {
       _sm.CurrentState.Update();
    }

    public void Grab(IInventory inventory)
    {
        _sm.ChangeState(_grabedState);
    }

    public void Active()
    {
        _sm.ChangeState(_targetSearch);
    }

    public void Removed()
    {
        _sm.ChangeState(_throwState);
    }
}
