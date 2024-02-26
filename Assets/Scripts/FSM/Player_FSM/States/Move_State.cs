using FSM;
using Movement;
using UnityEngine;

public class Move_State : State
{
    readonly IMovementStategy _movementStategy;

    public Move_State(IMovementStategy movementStategy)
    {
        _movementStategy = movementStategy;
    }

    public override void Enter()
    {
        JoystickMovementController.current.AddObserver(_movementStategy);
    }

    public override void Exit()
    {
        JoystickMovementController.current.RemoveObserver(_movementStategy);
    }
}
