using FSM;
using UnityEngine;

namespace Movement
{
    public sealed class JoystickMovementController : AObserver<IMovementStategy>, IInitialize, IPause
    {
        public static JoystickMovementController current;
        private Joystick _joystick;
        private Canvas joysticCanvas;
        public bool isInit { get; set; }
        public void Init()
        {
            current = this;
            _joystick = GetComponentInChildren<Joystick>();
            isInit = true;
        }
        public bool isPause { get; set; }
        public void Pause()
        {
            if (isPause)
            {
                joysticCanvas = transform.GetChild(0).transform.GetComponent<Canvas>();
                joysticCanvas.enabled = true;
                isPause = false;
            }
            else
            {
                DeactivateCanvas();
                isPause = true;
            }
        }
        public void DeactivateCanvas()
        {
            joysticCanvas = transform.GetChild(0).transform.GetComponent<Canvas>();
            joysticCanvas.enabled = false;
        }
        private void FixedUpdate()
        {
            if(!isInit) return;
            if(isPause) return;
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


