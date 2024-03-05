using UnityEngine;

public interface IBullet
{
    Transform target { get; set; }
    Transform bulletTransform { get; set; }
    void Hit();
    void Seek(Transform target);
    void SetEnchant(EnchantType enchantType);
}
