using System;
using UnityEngine;

public static class BulletFabric
{
    public static IBullet TryThrowItem(Character attacker, Item item)
    {
        switch (attacker.AmmoType)
        {
            case AmmoType.None:
            {
                Debug.LogError("Ammo type not set");
                return null;
            }
            case AmmoType.Oil:
            {
                GameObject bullet = InstanceHelper.InstantiatePrefab("Bullet",
                    attacker.Components.characterTransform.position,
                    attacker.Components.characterTransform.rotation);


                item.Transform.SetParent(bullet.transform);
                item.Transform.position = bullet.transform.position;

                var bulletGo = bullet.GetComponent<IBullet>();
                return bulletGo;
            }
            default:
            {
                Debug.LogError("No implementation for AmmoType: " + attacker.AmmoType);
                return null;
            }
        }
    }
}