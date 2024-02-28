using UnityEngine;

public interface IInventory
{
    void GrabItem(Transform transform, Item item);
    void RemoveItem(Transform transform, Item item);
}