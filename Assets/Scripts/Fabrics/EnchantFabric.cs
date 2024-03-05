
using UnityEngine;

public static class EnchantFabric
{
    
    public static EnchantmentDecorator CreateEnchant(Bullet bullet,EnchantType type)
    {
        switch (type)
        {
            case EnchantType.Oil:
            {
                return new OilEnchant(bullet);
            }
            case EnchantType.Fire:
            {
                return new FireEnchant(bullet);
            }
            default:
            {
                Debug.LogError("No Enchant of Type: " + type + " For Bullet: " + bullet);
                return null;
            }
        }
    }
    
}