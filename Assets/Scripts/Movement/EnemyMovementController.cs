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
        Move(player.transform.position);
    }
    
    private void Move(Vector3 direction)
    {
        foreach (var observer in observers)
        {
            observer.Move(direction);
        }
    }
}