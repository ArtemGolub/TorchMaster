using Movement;
using UnityEngine;

public class Follow_Strategy : IMovementStategy, IStrategy
{
    private Character _character;
    private float _speed;

    public Follow_Strategy(Character character)
    {
        _character = character;
    }
    
    public void Move(Vector3 direction)
    {
        if(_character == null) return;
        var dir = direction - _character.Components.characterTransform.position;
        Rotate(dir);
        Vector3 move = dir * (_character.Speed * Time.deltaTime);
        _character.Components.characterTransform.position += move;
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
        _character.Components.characterTransform.rotation = newRotation;
    }

    public void Subscribe()
    {
        EnemyMovementController.current.AddObserver(this);
    }

    public void UnSubscribe()
    {
        EnemyMovementController.current.RemoveObserver(this);
    }
}