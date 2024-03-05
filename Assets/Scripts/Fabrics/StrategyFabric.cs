using UnityEngine;


    public static class StrategyFabric
    {
        public static IStrategy CreateAttackStrategy(Character character,AttackType type)
        {
            switch (type)
            {
                case AttackType.None:
                {
                    return null;
                }
                case AttackType.Ranged:
                {
                    return new RangedAttack(character);
                }
                default:
                {
                   // Debug.LogError("No Character: " + attacker.ID + " or AmmoType: " + attacker.AmmoType);
                    return null;
                }
            }
        }
        
        public static IStrategy CreateMovementStrategy(Character character, MovementType type)
        {
            switch (type)
            {
                case MovementType.Walk:
                    return new Walk_Strategy(character);
                case MovementType.Follow:
                    return new Follow_Strategy(character);
                default:
                {
                    return null;
                }
            }
        }
    }
