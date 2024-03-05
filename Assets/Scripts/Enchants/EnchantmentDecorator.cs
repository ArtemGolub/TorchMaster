using UnityEngine;

public abstract class EnchantmentDecorator: IBullet
{
    private IBullet _bullet;
    public Transform target { get; set; }
    public Transform bulletTransform { get; set; }

    protected EnchantmentDecorator(IBullet bullet)
    {
        _bullet = bullet;
    }
    
    public virtual void Hit()
    {
        _bullet.Hit();
    }

    public void Seek(Transform target)
    {
        throw new System.NotImplementedException();
    }

    public void SetEnchant(EnchantType enchantType)
    {
        throw new System.NotImplementedException();
    }
}