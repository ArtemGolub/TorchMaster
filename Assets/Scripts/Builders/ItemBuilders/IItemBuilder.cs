using UnityEngine;

public interface IItemBuilder
{
    Item GetItem();
    void SetName(string name);
    void SetID(int id);

    void SetLifeTime(float time);
    void SetFSM(Transform transform, FSMType type);
}
