
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
                    return new Enemy_SM(character);
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
                    return new Oil_SM(item);
                case FSMType.TrueSight:
                    return new TrueSight_SM(item);
                case FSMType.Key:
                    return new Key_SM(item);
                default:
                    throw new ArgumentException("Invalid FSM type");
            }
        }
    }
