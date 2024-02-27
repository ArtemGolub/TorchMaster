using FSM;
using UnityEngine;

public class Torch_SM : StateMachine, IStateMachine
{
    private Torch torch;
    
    private StateMachine _sm;
    
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
        _burnState = new Burn_State(torch.item.LifeTime);
        _burnedState = new Burned_State(torch.transform);
        
        _sm.Initialize(_placedState);
    }

    public void UpdateBehaviour()
    {
        _sm.CurrentState.Update();
    }
}
