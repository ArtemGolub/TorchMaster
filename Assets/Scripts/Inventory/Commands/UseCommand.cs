
    public class UseCommand: IInventoryCommand
    {
        private IInventory _inventory;
        
        public UseCommand(IInventory inventory)
        {
            _inventory = inventory;
        }
        public void Execute(Item item)
        {
            if(item.FSM.CheckState(ItemStateType.Used)) return;
            item.FSM.ChangeState(ItemStateType.Used);
        }
    }
