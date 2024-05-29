using System.Collections.Generic;
using UnityEngine;

public class Enemy : ACharacter, IEnemy
{
   public List<Transform> patroolPoints;
   public bool canSeePlayer { get; set; }
    private void Start()
    {
        InitCharacter();
        _Character.patrolPoints = patroolPoints;
        Character.SM.InitBehaviour();
    }

    private void Update()
    {
        Character.SM.UpdateBehaviour();
    }
    
    
    // TODO Refactor
    public Character _Character
    {
        get
        {
            return Character;
        }
    }


}
