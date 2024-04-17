
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
        _character.CommandManager.AddCommand(CharacterCommandType.Move, _character.MovementType);
    }

    public void SetAttackType(AttackType type)
    {
        _character.AttackStrategy = StrategyFabric.CreateAttackStrategy(_character,type);
        _character.CommandManager.AddCommand(CharacterCommandType.Attack, _character.AttackStrategy);
    }
    
    public void SetFSM(FSMType type)
    {
        _character.SM = FSMFactory.CreateStrategy(_character, type);
    }

    public void SetInventory(InventoryType type)
    {
        _character.Inventory = ComponentFabric.CreateInventory(type, _character);
        _character.InventoryCommandManager = new InventoryCommandManager(_character.Inventory);
        
        _character.InventoryCommandManager.AddCommand(CharacterCommandType.Collect, new CollectCommand(_character.Inventory));
        _character.InventoryCommandManager.AddCommand(CharacterCommandType.Throw, new ThrowCommand(_character.Inventory));
        // TODO InventorySO
        _character.InventoryCommandManager.InitInventory(1,0 );
        
    }

    public void SetAmmoType(AmmoType type)
    {
        _character.AmmoType = type;
    }

    public void SetCollisions(CollisionTags[] tags)
    {
        foreach (var tag in tags)
        {
            CollisionObserverFabric.CreateCollisionObserver(_character, tag);
        }
    }

    public void SetFireObserver()
    {
        _character.TorchObserver = new TorchObserver(_character.Inventory);
    }

    public void SetOilObserver()
    {
        _character.OilObserver = new OilObserver(_character);
    }

    public void SetAttackRange(float range)
    {
        _character.attackRange = range;
    }

    public void SetRaloadTime(float time)
    {
        _character.raloadTime = time;
    }

    public Character GetCharacter()
    {
        return _character;
    }
}