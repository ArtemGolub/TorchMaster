using UnityEngine;

public class FireEnchant : EnchantmentDecorator
{
    private float damageRadius;
    private Bullet _bullet;

    public FireEnchant(Bullet bullet) : base(bullet)
    {
        _bullet = bullet;
        damageRadius = 5f;
    }

    public override void Hit()
    {
        Collider[] colliders = Physics.OverlapSphere(_bullet.target.position, damageRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Debug.Log(collider.gameObject.name);
                var character = collider.gameObject.GetComponent<Enemy>();
                if (!character) continue;
                character.Character.SM.ChancgeState(CharacterStateType.Fired);
            }
        }
    }
}