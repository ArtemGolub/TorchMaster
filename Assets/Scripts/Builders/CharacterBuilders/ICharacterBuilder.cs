using UnityEngine;

public interface ICharacterBuilder
{
    Character GetCharacter();
    void SetName(string name);
    void SetID(int id);
    void SetSpeed(float speed);
    void SetMovement(Transform transform, MovementType movementType);
    void SetFSM(Transform transform, FSMType type);
    void SetInventory(Transform transform, InventoryType type);

}