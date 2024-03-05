using Movement;
using UnityEngine;

public class Follow_Strategy : IMovementStategy, IStrategy
{
    private Transform _transform;
    private float _speed;

    public Follow_Strategy(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public void Move(Vector3 direction)
    {
        var dir = direction - _transform.position;
        Rotate(dir);
        Vector3 move = dir * (_speed * Time.deltaTime);
        _transform.position += move;
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
        _transform.rotation = newRotation;
    }

    public void Subscribe()
    {
        throw new System.NotImplementedException();
    }

    public void UnSubscribe()
    {
        throw new System.NotImplementedException();
    }
}