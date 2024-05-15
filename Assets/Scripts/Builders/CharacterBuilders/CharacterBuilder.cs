
using UnityEngine;
// TODO spread to different builders
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
        
        _character.CommandManager.AddCharacterCommand(CharacterCommandType.Use, new KeyCollectCommand());
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
        _character.CommandManager.AddCommand(CharacterCommandType.Follow, new Follow_Strategy(_character));
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

    public void SetInventory(InventoryType type, int capactiy)
    {
        _character.Inventory = ComponentFabric.CreateInventory(type, _character);
        _character.InventoryCommandManager = new InventoryCommandManager(_character.Inventory);
        
        _character.InventoryCommandManager.AddCommand(CharacterCommandType.Collect, new CollectCommand(_character.Inventory));
        _character.InventoryCommandManager.AddCommand(CharacterCommandType.Throw, new ThrowCommand(_character.Inventory));
        _character.InventoryCommandManager.AddCommand(CharacterCommandType.Use, new UseCommand(_character.Inventory));
        // TODO InventorySO
        _character.InventoryCommandManager.InitInventory(capactiy,0 );
        
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

    public void SetMadness(float maxMadness)
    {
        _character.maxMadness = maxMadness;
        _character.curMadness = maxMadness;
        
    }

    public void SetMadnessCommandManager()
    {
        _character.MadnessCommandManager = new MadnessCommandManager();
        
        _character.MadnessCommandManager.AddCommand(CharacterCommandType.EncreaseMadnessValue, new EncreaseMadness(_character));
        _character.MadnessCommandManager.AddCommand(CharacterCommandType.ReduceMadnessValue, new ReduceMadness(_character));
        
        _character.MadnessCommandManager.AddCommand(CharacterCommandType.EncreaseMadness, new EncreaseMadness(_character));
        _character.MadnessCommandManager.AddCommand(CharacterCommandType.ReduceMadness, new ReduceMadness(_character));
    }

    public void SetCharacterType(CharacterType type)
    {
        _character.CharacterType = type;
    }

    public void SetAnimationEventHandler(AnimationEventHandler animationEventHandler)
    {
        _character.AnimationEventHandler = animationEventHandler;
    }

    public Character GetCharacter()
    {
        return _character;
    }
}