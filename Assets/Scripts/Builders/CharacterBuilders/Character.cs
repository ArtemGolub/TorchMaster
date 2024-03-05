using System.Collections.Generic;
using FSM;


public class Character
{
    public string Name;
    
    public float Speed;
    
    public CharacterComponents Components;
    
    public CharacterCommandManager CommandManager;
    public InventoryCommandManager InventoryCommandManager;
    
    public CharacterType CharacterType;
    public AmmoType AmmoType;
    
    public IStrategy MovementType;
    public IStrategy AttackStrategy;
    
    public IInventory Inventory;
    
    public ICharacterStateMachine SM;


}