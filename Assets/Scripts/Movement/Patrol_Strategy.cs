using UnityEngine;

public class Patrol_Strategy : IPatrolStrategy, IStrategy
{
    private Character _character;

    private int _currentPatrolIndex = 0;
    
    public Patrol_Strategy(Character character )
    {
        _character = character;
        
    }

    public void Subscribe()
    {
        PatroolObserver.current.AddObserver(this);
    }

    public void UnSubscribe()
    {
        PatroolObserver.current.RemoveObserver(this);
    }
    
    public void Patrol()
    {
        if (_character == null || _character.Components == null || _character.Components.characterTransform == null)
        {
            Debug.LogWarning("Character or its components are null!");
            UnSubscribe();
            return;
        }

        if (_character.patrolPoints.Count == 0)
        {
            Debug.LogWarning("Patrol points list is empty!");
            return;
        }

        Vector3 currentPoint = _character.patrolPoints[_currentPatrolIndex].position;

        if (_character.Components.characterTransform == null)
        {
            Debug.LogWarning("Character transform is null!");
            return;
        }

        Vector3 direction = (currentPoint - _character.Components.characterTransform.position).normalized;

        Move(direction);

        float distanceToTarget = Vector3.Distance(_character.Components.characterTransform.position, currentPoint);
        if (distanceToTarget < 0.1f)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _character.patrolPoints.Count;
        }
    }
    public void Move(Vector3 direction)
    {
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
}
