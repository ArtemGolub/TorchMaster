using System;
using UnityEngine;

namespace Movement
{
    public sealed class JoystickMovementController : AObserver<IMovable>
    {
        private Joystick _joystick;

        private void Awake()
        {
            _joystick = GetComponentInChildren<Joystick>();
        }
        private void FixedUpdate()
        {
            Vector3 direction = new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y).normalized;
            if(direction == Vector3.zero) return;
            Move(direction);
        }
        private void Move(Vector3 direction)
        {
            foreach (var observer in observers)
            {
                observer.OnMove(direction);
            }
        }
    }
}


