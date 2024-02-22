using System;
using FSM;
using Movement;
using UnityEngine;

public class Player_SM : MonoBehaviour
{ 
    [SerializeField] private JoystickMovementController joystickMovementController;
    private MovementComponent _movementComponent;
    
    private StateMachine _sm;

    private Idle_State _idleState;
    private Move_State _moveState;

    void Start()
    {
        InitMovement();
        Init_FSM();
    }

    void Update()
    {
        _sm.CurrentState.Update();
    }
    
    void Init_FSM()
    {
        _sm = new StateMachine();
        _idleState = new Idle_State();
        _moveState = new Move_State(_movementComponent, joystickMovementController);
        
        _sm.Initialize(_moveState);
    }
    
    private void InitMovement()
    {
        _movementComponent = new MovementComponent(transform, 2f);
    }
}
