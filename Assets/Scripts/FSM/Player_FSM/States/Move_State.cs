using FSM;
using Movement;
using UnityEngine;

public class Move_State : State
{
    readonly MovementComponent _movementComponent;
    readonly JoystickMovementController _joystickMovementController;
    
    public Move_State(MovementComponent movementComponent, JoystickMovementController joystickMovementController)
    {
        _movementComponent = movementComponent;
        _joystickMovementController = joystickMovementController;
    }
    
    public override void Enter()
    {
        _joystickMovementController.AddObserver(_movementComponent);
    }

    public override void Exit()
    {
        _joystickMovementController.RemoveObserver(_movementComponent);
    }
}
