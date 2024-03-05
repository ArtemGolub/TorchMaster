public class ThrowCommand: IInventoryCommand
{
    private IInventory _inventory;
        
    public ThrowCommand(IInventory inventory)
    {
        _inventory = inventory;
    }
    
    public void Execute(Item item)
    {
        _inventory.RemoveItem(item);   
    }
}