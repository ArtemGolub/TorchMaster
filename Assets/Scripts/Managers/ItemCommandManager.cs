using System.Collections.Generic;
using UnityEngine;

public class ItemCommandManager
{
    private Dictionary<ItemCommandType, IStrategy> commands = new Dictionary<ItemCommandType, IStrategy>();

    public void AddCommand(ItemCommandType type, IStrategy command)
    {
        if(command == null) return;
        commands.Add(type, command);
    }
    
    public void SubscribeCommand(ItemCommandType type)
    {
        if (!commands.ContainsKey(type))
        {
            Debug.Log("No command find with type: " + type);
            return;
        }
        
        IStrategy command = commands[type];
        command.Subscribe();
    }

    public void UnSubscribeCommand(ItemCommandType type)
    {
        if (!commands.ContainsKey(type))
        {
            Debug.Log("No command find with type: " + type);
            return;
        }
        
        IStrategy command = commands[type];
        command.UnSubscribe();
    }
    
}