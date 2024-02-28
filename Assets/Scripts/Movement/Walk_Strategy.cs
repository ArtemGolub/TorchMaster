using Movement;
using UnityEngine;

public class Walk_Strategy : IMovementStategy
{
    private Transform _transform;
    private float _speed;

    public Walk_Strategy(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }
    public void Move(Vector3 direction)
    {
        Vector3 move = direction * (_speed * Time.deltaTime);
        _transform.position += move;
        
        Rotate(direction);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
            _transform.rotation = newRotation;
        }
    }
}