using System.Collections.Generic;
using FSM;
using UnityEngine;

// TODO Refactor
public class Character
{
    public string Name;
    
    public float Speed;
    public float attackRange;
    public float raloadTime;
    public float curMadness;
    public float maxMadness;
    
    public CharacterComponents Components;
    
    public CharacterCommandManager CommandManager;
    public InventoryCommandManager InventoryCommandManager;
    public MadnessCommandManager MadnessCommandManager;

    public TorchObserver TorchObserver;
    public OilObserver OilObserver;
    
    public CharacterType CharacterType;
    public AmmoType AmmoType;
    
    public IStrategy MovementType;
    public IStrategy AttackStrategy;
    
    public IInventory Inventory;
    
    public ICharacterStateMachine SM;

    // TODO Refactor
    public List<Transform> patrolPoints;
}