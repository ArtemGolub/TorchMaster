
    public class CollectCommand: IInventoryCommand
    {
        private IInventory _inventory;
        
        public CollectCommand(IInventory inventory)
        {
            _inventory = inventory;
        }

        public void Execute(Item item)
        {
            _inventory.AddItem(item);
        }
    }
