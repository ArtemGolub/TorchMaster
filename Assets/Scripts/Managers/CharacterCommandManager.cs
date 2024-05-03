using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandManager
{
    private Dictionary<CharacterCommandType, IStrategy> commands = new Dictionary<CharacterCommandType, IStrategy>();

    private Dictionary<CharacterCommandType, ICharacterCommand> _characterCommands =
        new Dictionary<CharacterCommandType, ICharacterCommand>();
    public void AddCommand(CharacterCommandType type, IStrategy command)
    {
        commands.Add(type, command);
    }
    public void AddCharacterCommand(CharacterCommandType type, ICharacterCommand command)
    {
        _characterCommands.Add(type, command);
    }
    
    public void SubscribeCommand(CharacterCommandType type)
    {
        if (!commands.ContainsKey(type))
        {
            Debug.Log("No command find with type: " + type);
            return;
        }
        
        IStrategy command = commands[type];
        command.Subscribe();
    }
    
    public void ExecuteCommand(CharacterCommandType characterCommandType, Character character = null)
    {
        if (_characterCommands.TryGetValue(characterCommandType, out ICharacterCommand command))
        {
            command.Execute(character);
        }
        else
        {
            Debug.Log("No command find with type: " + characterCommandType);
        }
    }
    public void UnSubscribeCommand(CharacterCommandType type)
    {
        if (!commands.ContainsKey(type))
        {
            Debug.Log("No command find with type: " + type);
            return;
        }
        
        IStrategy command = commands[type];
        command.UnSubscribe();
    }
    
    
    
    public void UnSubscribeAll()
    {
        foreach (var command in commands.Keys)
        {
            if (commands[command] == null) return;
            UnSubscribeCommand(command);
        }
    }
}
