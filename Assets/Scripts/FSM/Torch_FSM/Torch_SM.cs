using FSM;
using UnityEngine;

public class Torch_SM : StateMachine, IItemStateMachine
{
    private Item _item;
    
    private StateMachine _sm;

    private Grabed_State _grabedState;
    private Burn_State _burnState;
    private Placed_State _placedState;
    private Burned_State _burnedState;
    
    public Torch_SM(Item item)
    {
        _item = item;
    }
    public void InitBehaviour()
    {
        _sm = new StateMachine();
        
        _placedState = new Placed_State();
        _grabedState = new Grabed_State(_item);
        _burnState = new Burn_State(_item, _item.Transform);
        _burnedState = new Burned_State(_item.Transform);
        
        _sm.Initialize(_placedState);
    }

    public void UpdateBehaviour()
    {
        _sm.CurrentState.Update();
    }

    public void Grab(IInventory inventory)
    {
        _burnState.SetInventory(inventory);
        _sm.ChangeState(_grabedState);
    }

    public void Active()
    {
        _sm.ChangeState(_burnState);
    }

    public void Removed()
    {
        _sm.ChangeState(_burnedState);
    }

    public void Seek(Transform target)
    {
        return;
    }
}
