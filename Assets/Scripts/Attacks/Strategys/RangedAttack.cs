using UnityEngine;

public class RangedAttack : IAttackStrategy, IStrategy
{
    private Character _attacker;
    private Transform _target;
    private float raloadTime;
    
    public RangedAttack(Character attacker)
    {
        _attacker = attacker;
    }

    private void TryAttack()
    {
        if(_attacker.OilObserver.GetActiveOil() == null) return;
        if(!IsInRange()) return;
        Shoot();
    }
    private Transform TryFindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(GetTargetTag(_attacker.CharacterType));
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
        
        if (nearestEnemy != null && shortestDistance <= _attacker.attackRange)
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
        if (raloadTime <= 0)
        {
            raloadTime = 1f / _attacker.raloadTime;
            Item activeAmmo = _attacker.OilObserver.GetActiveOil();
            if(activeAmmo == null) return;
            _attacker.InventoryCommandManager.ExecuteCommand(CharacterCommandType.Throw, activeAmmo);
            var bullet = BulletFabric.TryThrowItem(_attacker, activeAmmo);
            
            if (_attacker.TorchObserver.IsTorchBurning())
            {
                bullet.SetEnchant(EnchantType.Fire);
            }
            else
            {
                bullet.SetEnchant(EnchantType.Oil);
            }
            
            
            bullet.Seek(_target);
            
        }
        if (raloadTime < 0)
        {
            raloadTime = 1f / _attacker.raloadTime;
        }
        raloadTime -= Time.deltaTime;
      
    }

    public void Subscribe()
    {
        RangeObserver.current.AddObserver(this);
    }

    public void UnSubscribe()
    {
        RangeObserver.current.RemoveObserver(this);
    }
    public void Attack()
    {
        TryAttack();
    }

    private string GetTargetTag(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Player:
            {
                return "Enemy";
            }
            case CharacterType.Enemy:
            {
                return "Player";
            }
            default:
            {
                Debug.LogError("No Target Type for: " + type);
                return "";
            }
        }
    }
}
