using UnityEngine;

public interface IAttackStrategy
{
    void TryAttack(Transform attacker);
    bool IsInRange();
}
