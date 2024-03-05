using UnityEngine;

public static class BulletFabric
{

    public static void TryThrowItem(Character attacker,AmmoType type, Transform target)
    {
        switch (type)
        {
            case AmmoType.None:
            {
                Debug.Log("get null");
                return;
            }
            case AmmoType.Oil:
            {
                Debug.Log("get oil");
               // attacker.Inventory.ThrowItem(ItemType.Oil, target);
                return;
            }
        }
    }
}
