using FSM;
using Movement;

public class Player_SM: StateMachine
{
    private IMovable _movementComponent;
    private JoystickMovementController _joystickMovementController;
    
    private StateMachine _sm;

    private Idle_State _idleState;
    private Move_State _moveState;

    public Player_SM(IMovable movementComponent, JoystickMovementController joystickMovementController)
    {
        _movementComponent = movementComponent;
        _joystickMovementController = joystickMovementController;

        Init_FSM();
    }
    
    void Init_FSM()
    {
        _sm = new StateMachine();
        _idleState = new Idle_State();
        _moveState = new Move_State(_movementComponent, _joystickMovementController);
        
        _sm.Initialize(_moveState);
    }
}
