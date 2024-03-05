using UnityEngine;

public interface IItemBuilder
{
    Item GetItem();
    void SetName(string name);
    void SetItemType(ItemType type);
    void SetTransform(Transform transform);
    void SetCollider(Collider collider);
    void SetFSM(FSMType type);
}
