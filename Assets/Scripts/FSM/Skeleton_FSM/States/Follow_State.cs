using FSM;
using Movement;
public class Follow_State : State
{
    readonly IMovementStategy _movementStategy;
    
    public Follow_State(IMovementStategy movementStategy)
    {
        _movementStategy = movementStategy;
    }

    public override void Enter()
    {
        EnemyMovementController.current.AddObserver(_movementStategy);
    }

    public override void Exit()
    {
        EnemyMovementController.current.RemoveObserver(_movementStategy);
    }
}
