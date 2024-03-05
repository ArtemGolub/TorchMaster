public interface IInventory
{
    void AddItem(Item item);
    void RemoveItem(Item item);
    void SetInventoryCapacity(int torchCapacity, int oilCapacity);
}