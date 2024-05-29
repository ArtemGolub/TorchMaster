using System;
using Movement;
using UnityEngine;

public class EnemyMovementController: AObserver<IMovementStategy>, IInitialize, IPause
{
    public static EnemyMovementController current;
    [HideInInspector]public Player player;
    public bool isInit { get; set; }

    private void Awake()
    {
        current = this;
    }

    public void Init()
    {
        
        isInit = true;
    }
    public bool isPause { get; set; }
    public void Pause()
    {
        if (isPause)
        {
            isPause = false;
        }
        else
        {
            isPause = true;
        }
    }
    private void FixedUpdate()
    {
        if(!isInit) return;
        if(isPause) return;
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