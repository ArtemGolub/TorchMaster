using UnityEngine;

public class Item
{
    public string Name;

    public ItemType ItemType;

    public ItemCommandManager ItemCommandManager;
    
    public Transform Transform;
    public Collider Collider;

    public IItemStateMachine FSM;
}