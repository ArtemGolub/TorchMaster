using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryVisualaser
{
    private Inventory _inventory;

    public InventoryVisualaser(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void PlaceItem(Item item)
    {
        item.Transform.SetParent(_inventory._transform);
        item.Transform.position = _inventory._transform.position;
        AdjustItemPositions();
    }

    public void RemoveItem(Item item)
    {
        item.Transform.SetParent(null);
    }

    private bool Intersects(Item item1, Item item2)
    {
        return (item1.Transform.position.x < item2.Transform.position.x + item2.Collider.bounds.size.x &&
                item1.Transform.position.x + item1.Collider.bounds.size.x > item2.Transform.position.x &&
                item1.Transform.position.y < item2.Transform.position.y + item2.Collider.bounds.size.y &&
                item1.Transform.position.y + item1.Collider.bounds.size.y > item2.Transform.position.y);
    }

    private void AdjustItemPositions()
    {
        List<Item> allItems = _inventory._items.SelectMany(pair => pair.Value).ToList();
        allItems = allItems.OrderBy(item => item.Transform.position.y).ToList();
        
        for (int i = 1; i < allItems.Count; i++)
        {
            Item currentItem = allItems[i];
            Item previousItem = allItems[i - 1];
            
            if (Intersects(currentItem, previousItem))
            {
                currentItem.Transform.position = new Vector3(currentItem.Transform.position.x,
                    previousItem.Transform.position.y + previousItem.Collider.bounds.size.y + 0.01f,
                    currentItem.Transform.position.z);
                
                for (int j = i - 1; j >= 0; j--)
                {
                    previousItem = allItems[j];
                    if (Intersects(currentItem, previousItem))
                    {
                        currentItem.Transform.position = new Vector3(currentItem.Transform.position.x,
                            previousItem.Transform.position.y + previousItem.Collider.bounds.size.y + 1,
                            currentItem.Transform.position.z);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}