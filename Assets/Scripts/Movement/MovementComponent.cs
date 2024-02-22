using UnityEngine;

namespace Movement
{
    public sealed class MovementComponent : IMovable
    {
         readonly Transform _transform;
         readonly float _speed;

         public MovementComponent(Transform transform, float speed)
         {
             _transform = transform;
             _speed = speed;
         }
         
        public void OnMove(Vector3 direction)
        {
            Vector3 move = direction * (_speed * Time.deltaTime);
            
            _transform.position += move;
        }
    }
}

