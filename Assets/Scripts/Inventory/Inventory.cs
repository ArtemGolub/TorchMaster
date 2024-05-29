using System.Collections.Generic;
using UnityEngine;


public sealed class Inventory : IInventory
{
    public Transform _transform;
    private InventoryVisualaser _visualaser;
    public Dictionary<ItemType, ItemList<Item>> _items { get; set; }
    
    public Inventory(Transform inventoryPosition)
    {
        _transform = inventoryPosition;
        _visualaser = new InventoryVisualaser(this);
        _items = new Dictionary<ItemType, ItemList<Item>>();
    }
    
    public bool AddItem(Item item)
    {
        if (_items.ContainsKey(item.ItemType))
        {
            if (_items[item.ItemType].Add(item))
            {
                _visualaser.PlaceItem(item);
                UpdateTorchCount();
                return true;
            }
        }
        AudioManager.current.PlayPlayerSpeak(SoundType.ICantHandleMore);
        return false;
    }

    public void RemoveItem(Item item)
    {
        if(item == null) {return;}
        if (_items.ContainsKey(item.ItemType))
        {
            _items[item.ItemType].Remove(item);
            _visualaser.RemoveItem(item);
            UpdateTorchCount();
        }
    }

    public void SetInventoryCapacity(int torchCapacity, int oilCapacity)
    {
        _items[ItemType.Torch] = new ItemList<Item>(torchCapacity);
        _items[ItemType.Oil] = new ItemList<Item>(oilCapacity);
        UpdateTorchCount();
    }
    
    private void UpdateTorchCount()
    {
        if (_items.ContainsKey(ItemType.Torch))
        {
            int torchCount = _items[ItemType.Torch].Count;
            TorchCanvas.current.UpdateTorchCount(torchCount, _items[ItemType.Torch].Capacity);
        }
    }
}