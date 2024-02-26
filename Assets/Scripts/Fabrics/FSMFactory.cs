
    using System;
    using FSM;
    using UnityEngine;

    public static class FSMFactory
    {
        public static IStateMachine CreateStrategy(Transform transform,FSMType type)
        {
            switch (type)
            {
                case FSMType.Player:
                    return new Player_SM(transform);
                default:
                    throw new ArgumentException("Invalid movement type");
            }
        }
    }
