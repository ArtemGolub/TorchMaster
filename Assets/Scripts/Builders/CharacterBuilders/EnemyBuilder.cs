using UnityEngine;

public class EnemyBuilder: ICharacterBuilder
{
    private Character _character = new Character();


    public void SetName(string name)
    {
        _character.Name = name;
    }

    public void SetID(int id)
    {
        _character.ID = id;
    }

    public void SetSpeed(float speed)
    {
        _character.Speed = speed;
    }

    public void SetMovement(Transform transform, MovementType type)
    {
        _character.MovementType = StrategyFactory.CreateStrategy(transform, _character.Speed, type);
    }

    public void SetFSM(Transform transform, FSMType type)
    {
        _character.SM = FSMFactory.CreateStrategy(transform, type);
    }

    public void SetInventory(Transform transform, InventoryType type)
    {
        _character.Inventory = InventoryFabric.CreateInventory(transform, type);
    }

    public Character GetCharacter()
    {
        return _character;
    }
}
