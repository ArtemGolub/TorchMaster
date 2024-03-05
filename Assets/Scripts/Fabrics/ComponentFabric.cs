using System;

public static class ComponentFabric
{
    public static IInventory CreateInventory(InventoryType type, Character character)
    {
        switch (type)
        {
            case InventoryType.Player:
                return new Inventory(character.Components.inventoryTransform);
            case InventoryType.Skeleton:
                return null;
            default:
                throw new ArgumentException("Invalid FSM type");
        }
    }
    
}