using FSM;
using UnityEngine;

namespace Movement
{
    public sealed class JoystickMovementController : AObserver<IMovementStategy>
    {
        public static JoystickMovementController current;
        private Joystick _joystick;
        private Canvas joysticCanvas;

        public void DeactivateCanvas()
        {
            joysticCanvas = transform.GetChild(0).transform.GetComponent<Canvas>();
            joysticCanvas.enabled = false;
        }
        private void Awake()
        {
            current = this;
            _joystick = GetComponentInChildren<Joystick>();
        }
        private void FixedUpdate()
        {
            if(observers == null) return;
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
            for (int i = 0; i < observers.Count; i++)
            {
                if(observers[i] == null) return;
                observers[i].Move(direction);
            }

        }
    }
}


