using FSM;
using Movement;
using UnityEngine;

public class Move_State : State, IMovable
{
    readonly Transform _player;
    readonly JoystickMovementController _joystickMovementController;
    
    public Move_State(Transform player,JoystickMovementController joystickMovementController)
    {
        _player = player;
        _joystickMovementController = joystickMovementController;
    }
    
    public override void Enter()
    {
        _joystickMovementController.AddObserver(this);
    }

    public override void Exit()
    {
        _joystickMovementController.RemoveObserver(this);
    }
    
    public void OnMove(Vector3 direction)
    {
        _player.transform.position += direction;
    }
}
