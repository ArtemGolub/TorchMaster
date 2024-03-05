using UnityEngine;


    public static class StrategyFabric
    {
        public static IStrategy CreateAttackStrategy(AttackType type)
        {
            switch (type)
            {
                case AttackType.None:
                {
                    return null;
                }
                case AttackType.Ranged:
                {
                    return null; //new RangedAttack();
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
                    return null; //new Follow_Strategy();
                default:
                {
                    return null;
                }
            }
        }
    }
