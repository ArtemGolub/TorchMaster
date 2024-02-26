using System;
using Movement;
using UnityEngine;

public static class StrategyFactory
{
    public static IMovementStategy CreateStrategy(Transform transform, float speed, MovementType type)
    {
        switch (type)
        {
            case MovementType.Walk:
                return new Walk_Strategy(transform, speed);
            default:
                throw new ArgumentException("Invalid movement type");
        }
    }
}