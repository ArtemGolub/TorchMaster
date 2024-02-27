
    using System;
    using FSM;
    using UnityEngine;

    public static class FSMFactory
    {
        public static ICharacterStateMachine CreateStrategy(Transform transform,FSMType type)
        {
            switch (type)
            {
                case FSMType.Player:
                    return new Player_SM(transform);
                default:
                    throw new ArgumentException("Invalid FSM type");
            }
        }

        public static IItemStateMachine CreateItemStrategy(Transform transform,FSMType type)
        {
            switch (type)
            {
                case FSMType.Torch:
                    return new Torch_SM(transform);
                default:
                    throw new ArgumentException("Invalid FSM type");
            }
        }
    }
