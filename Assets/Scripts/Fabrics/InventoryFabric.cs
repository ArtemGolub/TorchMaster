using System;
using UnityEngine;

public static class InventoryFabric
{
    public static IInventory CreateInventory(Transform inventoryPose,InventoryType type)
    {
        switch (type)
        {
            case InventoryType.Player:
                return new PlayerInventory(inventoryPose);
            case InventoryType.Skeleton:
                return null;
            default:
                throw new ArgumentException("Invalid FSM type");
        }
    }
    
}