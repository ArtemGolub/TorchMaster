using System.Collections.Generic;
using FSM;

public class Torch_SM : StateMachine, IItemStateMachine
{
    private Item _item;
    
    private StateMachine _sm;
    private Dictionary<ItemStateType, State> _states = new Dictionary<ItemStateType, State>();
    
    public Torch_SM(Item item)
    {
        _item = item;
    }
    public void InitBehaviour()
    {
        _sm = new StateMachine();

        AddState(ItemStateType.Idle, new Item_Idle_State());
        AddState(ItemStateType.Grab, new Item_Grabed_State(_item));
        AddState(ItemStateType.Active, new Item_Active_State(_item));
        AddState(ItemStateType.Used, new Item_Used_State(_item));
        
        _sm.Initialize(_states[ItemStateType.Idle]);
    }
    public void UpdateBehaviour()
    {
        _sm.CurrentState.Update();
    }
    
    public void ChangeState(ItemStateType characterStateType)
    {
        _sm.ChangeState(_states[characterStateType]);
    }

    public bool CheckState(ItemStateType stateType)
    {
        return _states[stateType] == _sm.CurrentState;
    }

    private void AddState(ItemStateType characterStateType, State state)
    {
        _states[characterStateType] = state;
    }
}
