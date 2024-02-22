using Movement;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private JoystickMovementController joystickMovementController;
    MovementComponent _movementComponent;
    void Start()
    {
        InitMovement();
    }
    void InitMovement()
    {
        _movementComponent = new MovementComponent(this.transform, 2f);
        joystickMovementController = FindObjectOfType<JoystickMovementController>();
        joystickMovementController.AddObserver(_movementComponent);
    }

    void RemoveMovement()
    {
        joystickMovementController.RemoveObserver(_movementComponent);
    }
}
