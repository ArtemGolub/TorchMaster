using UnityEngine;

public sealed class ItemCollisionHandler : ICollisionHandler<IItem>
{
    readonly Character _character;

    public ItemCollisionHandler(Character character)
    {
        _character = character;
    }

    public void HandleCollision(IItem collidedObject)
    {
        _character.InventoryCommandManager.ExecuteCommand(CharacterCommandType.Collect, collidedObject.item);
    }

    public void HandleCollisionExit(IItem collidedObject)
    {
        
    }
}