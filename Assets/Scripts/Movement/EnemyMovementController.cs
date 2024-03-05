using Movement;
using UnityEngine;

public class EnemyMovementController: AObserver<IMovementStategy>
{
    public static EnemyMovementController current;
    public Player player;

    private void Awake()
    {
        current = this;
    }
    
    private void FixedUpdate()
    {
        if(player == null) return;
        if(observers == null) return;
        Move(player.transform.position);
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