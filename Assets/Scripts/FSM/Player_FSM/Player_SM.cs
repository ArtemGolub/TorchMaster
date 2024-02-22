using System;
using FSM;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

public class Player_SM : MonoBehaviour
{
    [FormerlySerializedAs("movementController")] [FormerlySerializedAs("movementObserver")] [FormerlySerializedAs("joystick")] [SerializeField] private JoystickMovementController joystickMovementController;
    
    private StateMachine _sm;
    private Move_State _moveState;
   

    private void Start()
    {
        Init_FSM();
    }

    private void Update()
    {
        _sm.CurrentState.Update();
    }
    
    private void Init_FSM()
    {
        _sm = new StateMachine();
        _moveState = new Move_State(this.transform, joystickMovementController);
        
        _sm.Initialize(_moveState);
    }
}
