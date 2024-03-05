using System.Collections.Generic;
using FSM;
using UnityEngine;


public class Character
{
    public string Name;
    
    public float Speed;
    public float attackRange;
    public float raloadTime;
    
    public CharacterComponents Components;
    
    public CharacterCommandManager CommandManager;
    public InventoryCommandManager InventoryCommandManager;

    public TorchObserver TorchObserver;
    public OilObserver OilObserver;
    
    public CharacterType CharacterType;
    public AmmoType AmmoType;
    
    public IStrategy MovementType;
    public IStrategy AttackStrategy;
    
    public IInventory Inventory;
    
    public ICharacterStateMachine SM;

    public List<Transform> patrolPoints;


}