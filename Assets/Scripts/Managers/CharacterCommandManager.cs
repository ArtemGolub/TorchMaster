using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandManager
{
    private Dictionary<CommandType, IStrategy> commands = new Dictionary<CommandType, IStrategy>();

    public void AddCommand(CommandType type, IStrategy command)
    {
        commands.Add(type, command);
    }
    
    public void SubscribeCommand(CommandType type)
    {
        if (!commands.ContainsKey(type))
        {
            Debug.Log("No command find with type: " + type);
            return;
        }
        
        IStrategy command = commands[type];
        command.Subscribe();
    }

    public void UnSubscribeCommand(CommandType type)
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
