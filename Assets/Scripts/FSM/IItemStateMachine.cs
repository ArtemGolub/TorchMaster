using FSM;

public interface IItemStateMachine
{
    public void InitBehaviour();
    public void UpdateBehaviour();
    public void ChangeState(ItemStateType characterStateType);
    public bool CheckState(ItemStateType stateType);
}