using UnityEngine;

public class RangedAttack : IAttackStrategy, IStrategy
{
    private Character _attacker;
    private Transform _target;
    
    public RangedAttack(Character attacker, AmmoType ammoType)
    {
        _attacker = attacker;
    }

    public void TryAttack(Transform attacker)
    {
        if(!IsInRange()) return;
        Shoot();
    }
    private Transform TryFindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;
        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;
        
        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(_attacker.Components.characterTransform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        
        if (nearestEnemy != null && shortestDistance <= 5f)
        {
            _target = nearestEnemy.transform;
            return _target;
        }
        else
        {
            return null;
        }
    }
    public bool IsInRange()
    {
        var targetEnemy = TryFindTarget();
        if (targetEnemy == null) return false;
        return true;
    }
    private void Shoot()
    {
        if (_target == null) return;
        Debug.Log("shoot");
        BulletFabric.TryThrowItem(_attacker, _attacker.AmmoType, _target); 
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
