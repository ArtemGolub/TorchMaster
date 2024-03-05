using System.Collections.Generic;

public interface IInventory
{
    public Dictionary<ItemType, ItemList<Item>> _items { get; set; }
    void AddItem(Item item);
    void RemoveItem(Item item);
    void SetInventoryCapacity(int torchCapacity, int oilCapacity);
}