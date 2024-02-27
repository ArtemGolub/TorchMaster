using UnityEngine;

public interface IInventory
{
    void GrabItem(Transform transform, Item item);
    void ThrowItem(Item item);
}