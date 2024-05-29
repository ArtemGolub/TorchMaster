using System.Collections.Generic;
using UnityEngine;

public interface IItemBuilder
{
    Item GetItem();
    void SetName(string name);
    void SetItemType(ItemType type);
    void SetTransform(Transform transform);
    void SetCollider(Collider collider);
    void SetItemCommandManager(ItemSO itemSo);
    void SetFSM(FSMType type);
    void SetLightPoints(List<Transform> lightPoints);
    void SetCollectSound(AudioSource sound);
    void SetCantCollectSound(AudioSource sound);
}
