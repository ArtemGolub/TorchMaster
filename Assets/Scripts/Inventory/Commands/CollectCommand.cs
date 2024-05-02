
    public class CollectCommand: IInventoryCommand
    {
        private IInventory _inventory;
        
        public CollectCommand(IInventory inventory)
        {
            _inventory = inventory;
        }

        public void Execute(Item item)
        {
            if(item.FSM.CheckState(ItemStateType.Used)) return;
            if (_inventory.AddItem(item))
            {
                item.FSM.ChangeState(ItemStateType.Grab);
            }
        }
    }
