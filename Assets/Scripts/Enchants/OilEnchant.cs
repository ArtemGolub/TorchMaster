using UnityEngine;

public class OilEnchant : EnchantmentDecorator
{
    private float damageRadius = 25f;
    private IBullet _bullet;

    public OilEnchant(IBullet bullet) : base(bullet)
    {
        _bullet = bullet;
        damageRadius = 25f;
    }

    public override void Hit()
    {
        Collider[] colliders = Physics.OverlapSphere(_bullet.target.position, damageRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                var character = collider.gameObject.GetComponent<Enemy>();
                if (!character) return;
                character.Character.SM.ChancgeState(CharacterStateType.Slowed);
            }
        }
    }

}