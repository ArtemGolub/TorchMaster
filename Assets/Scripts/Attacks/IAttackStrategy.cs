using UnityEngine;

public interface IAttackStrategy
{
    bool IsInRange();
    void Attack();
}
