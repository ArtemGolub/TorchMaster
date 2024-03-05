
using UnityEngine;

public class CharacterBuilder : ICharacterBuilder
{
    private Character _character = new Character();

    public void SetComponents(CharacterComponents components)
    {
        _character.Components = components;
    }

    public void SetCommandManager()
    {
        _character.CommandManager = new CharacterCommandManager();
    }

    public void SetName(string name)
    {
        _character.Name = name;
    }
    public void SetSpeed(float speed)
    {
        _character.Speed = speed;
    }
    
    public void SetMovement(MovementType type)
    {
        _character.MovementType = StrategyFabric.CreateMovementStrategy(_character, type);
        _character.CommandManager.AddCommand(CommandType.Move, _character.MovementType);
    }

    public void SetAttackType(AttackType type)
    {
        _character.AttackStrategy = StrategyFabric.CreateAttackStrategy(type);
        _character.CommandManager.AddCommand(CommandType.Attack, _character.AttackStrategy);
    }
    
    public void SetFSM(FSMType type)
    {
        _character.SM = FSMFactory.CreateStrategy(_character, type);
    }

    public void SetInventory(InventoryType type)
    {
        _character.Inventory = ComponentFabric.CreateInventory(type, _character);
        _character.InventoryCommandManager = new InventoryCommandManager(_character.Inventory);
        
        _character.InventoryCommandManager.AddCommand(CommandType.Collect, new CollectCommand(_character.Inventory));
        
    }

    public void SetAmmoType(AmmoType type)
    {
        _character.AmmoType = type;
    }

    public Character GetCharacter()
    {
        return _character;
    }
}