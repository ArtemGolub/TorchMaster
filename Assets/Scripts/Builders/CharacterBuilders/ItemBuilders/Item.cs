using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string Name;
    public List<Transform> LightPoint;
    
    public ItemType ItemType;

    public ItemCommandManager ItemCommandManager;
    
    public Transform Transform;
    public Collider Collider;

    public IItemStateMachine FSM;
}