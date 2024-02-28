using FSM;
using UnityEngine;

public class Torch_SM : StateMachine, IItemStateMachine
{
    private Torch torch;
    private IInventory _inventory;
    
    private StateMachine _sm;

    private Grabed_State _grabedState;
    private Burn_State _burnState;
    private Placed_State _placedState;
    private Burned_State _burnedState;
    
    public Torch_SM(Transform transform)
    {
        torch = transform.GetComponent<Torch>();
    }
    public void InitBehaviour()
    {
        _sm = new StateMachine();
        
        _placedState = new Placed_State();
        _grabedState = new Grabed_State(torch.item);
        _burnState = new Burn_State(torch.item, torch.transform);
        _burnedState = new Burned_State(torch.transform);
        
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
}
