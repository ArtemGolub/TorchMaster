
    using System;
    using System.Collections.Generic;

    public class InventoryCommandManager
    {
        private readonly IInventory _inventory;
        private Dictionary<CharacterCommandType, IInventoryCommand> commands = new Dictionary<CharacterCommandType, IInventoryCommand>();
        
        public InventoryCommandManager(IInventory inventory)
        {
            _inventory = inventory;
        }
        public void InitInventory(int torchCapacity, int oilCapacity)
        {
            _inventory.SetInventoryCapacity(torchCapacity, oilCapacity);
        }
        
        public void AddCommand(CharacterCommandType characterCommandType, IInventoryCommand inventoryCommand)
        {
            commands.Add(characterCommandType, inventoryCommand);
        }
        public void ExecuteCommand(CharacterCommandType characterCommandType, Item item = null)
        {
            if (commands.TryGetValue(characterCommandType, out IInventoryCommand strategy))
            {
                strategy.Execute(item);
            }
            else
            {
                Console.WriteLine("Command not found.");
            }
        }

        public Item GetItem(ItemType itemType)
        {
            if (_inventory._items.ContainsKey(itemType))
            {
                ItemList<Item> itemList = _inventory._items[itemType];
                if (itemList != null && itemList.Count > 0)
                {
                    return itemList[0];
                }
            }
            return null;
        }
    }
