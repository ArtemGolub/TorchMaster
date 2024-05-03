using System;
using System.Collections.Generic;
using UnityEngine;

public class MadnessCommandManager
{
    private Dictionary<CharacterCommandType, IMadnessCommand> commands = new Dictionary<CharacterCommandType, IMadnessCommand>();
    
    public void AddCommand(CharacterCommandType characterCommandType, IMadnessCommand madnessCommand)
    {
        commands.Add(characterCommandType, madnessCommand);
    }
    public void ExecuteCommand(CharacterCommandType characterCommandType, float value)
    {
        if (commands.TryGetValue(characterCommandType, out IMadnessCommand command))
        {
            command.ExecuteValue(value);
        }
        else
        {
            Console.WriteLine("Command not found.");
        }
    }

    public void SubscribeCommand(CharacterCommandType characterCommandType)
    {
        if (commands.TryGetValue(characterCommandType, out IMadnessCommand command))
        {
            if (!MadnessObserver.current.ContainObserver(command))
            {
                MadnessObserver.current.AddObserver(command);
                Debug.Log("subscribed: " + characterCommandType);
            }
        }
    }

    public void UnSubscribeCommand(CharacterCommandType characterCommandType)
    {
        if (commands.TryGetValue(characterCommandType, out IMadnessCommand command))
        {
            if (MadnessObserver.current.ContainObserver(command))
            {
                MadnessObserver.current.RemoveObserver(command);
                Debug.Log("unsubscribed: " + characterCommandType);
            }
        }
    }
}