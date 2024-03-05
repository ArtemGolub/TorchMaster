using Movement;
using UnityEngine;

public class Walk_Strategy : IMovementStategy, IStrategy
{
     Character _character;
     private bool subscribed;
    public Walk_Strategy(Character character)
    {
        _character = character;
    }
    
    public void Move(Vector3 direction)
    {
        if(!subscribed) return;
        Vector3 move = direction * (_character.Speed * Time.deltaTime);
        _character.Components.characterTransform.position += move;
        
        Rotate(direction);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
            _character.Components.characterTransform.rotation = newRotation;
        }
    }

    public void Subscribe()
    {
        JoystickMovementController.current.AddObserver(this);
        subscribed = true;
    }

    public void UnSubscribe()
    {
        JoystickMovementController.current.RemoveObserver(this);
        subscribed = false;
    }
}