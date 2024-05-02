using UnityEngine;

public class LightingCollisionHandler: ICollisionHandler<ILightingPoint>
{
    readonly Character _character;

    public LightingCollisionHandler(Character character)
    {
        _character = character;
    }
    public void HandleCollision(ILightingPoint collidedObject)
    {
        _character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.ReduceMadness);
        _character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.EncreaseMadness);
    }

    public void HandleCollisionExit(ILightingPoint collidedObject)
    {
        _character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.EncreaseMadness);
        _character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.ReduceMadness);
    }
}