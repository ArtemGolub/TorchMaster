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
        if(!collidedObject.myLight.enabled) return;
        _character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.ReduceMadness);
        _character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.EncreaseMadness);
    }

    public void HandleCollisionExit(ILightingPoint collidedObject)
    {
        _character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.EncreaseMadness);
        _character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.ReduceMadness);
    }

    public void HandleCollisionStay(ILightingPoint collidedObject)
    {
        if(!collidedObject.myLight.enabled) return;
        _character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.ReduceMadness);
        _character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.EncreaseMadness);
    }
}