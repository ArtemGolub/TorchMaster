
    using System;
    using System.Collections.Generic;

    public class InventoryCommandManager
    {
        private readonly IInventory _inventory;
        private Dictionary<CommandType, IInventoryCommand> commands = new Dictionary<CommandType, IInventoryCommand>();
        
        public InventoryCommandManager(IInventory inventory)
        {
            _inventory = inventory;
        }
        public void InitInventory(int torchCapacity, int oilCapacity)
        {
            _inventory.SetInventoryCapacity(torchCapacity, oilCapacity);
        }
        public void CollectItem(Item item)
        {
            _inventory.AddItem(item);
        }
        public void RemoveItem(Item item)
        {
            _inventory.RemoveItem(item);
        }

        public void AddCommand(CommandType commandType, IInventoryCommand inventoryCommand)
        {
            commands.Add(commandType, inventoryCommand);
        }
        public void ExecuteCommand(CommandType commandType, Item item = null)
        {
            if (commands.TryGetValue(commandType, out IInventoryCommand strategy))
            {
                strategy.Execute(item);
            }
            else
            {
                Console.WriteLine("Command not found.");
            }
        }
    }
