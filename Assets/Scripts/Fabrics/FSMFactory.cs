
    using System;
    using FSM;
    using UnityEngine;

    public static class FSMFactory
    {
        public static ICharacterStateMachine CreateStrategy(Character character,FSMType type)
        {
            switch (type)
            {
                case FSMType.Player:
                    return new Player_SM(character);
                case FSMType.Skeleton:
                    return null;
                default:
                    throw new ArgumentException("Invalid FSM type");
            }
        }

        public static IItemStateMachine CreateItemStrategy(Item item,FSMType type)
        {
            switch (type)
            {
                case FSMType.Torch:
                    return new Torch_SM(item);
                case FSMType.Oil:
                    return null;
                default:
                    throw new ArgumentException("Invalid FSM type");
            }
        }
    }
