using FSM;
using UnityEngine;

namespace Movement
{
    public sealed class JoystickMovementController : AObserver<IMovementStategy>
    {
        public static JoystickMovementController current;
        private Joystick _joystick;
        
        private void Awake()
        {
            current = this;
            _joystick = GetComponentInChildren<Joystick>();
        }
        private void FixedUpdate()
        {
            Vector3 direction = new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y).normalized;
            if (direction == Vector3.zero)
            {
                CharacterFSMObserver.current.IdleState();
                return;
            }
            
            CharacterFSMObserver.current.MoveState();
            Move(direction);
        }
        private void Move(Vector3 direction)
        {
            foreach (var observer in observers)
            {
                observer.Move(direction);
            }
        }
    }
}


