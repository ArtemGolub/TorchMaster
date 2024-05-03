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
        if (collidedObject.item.ItemType == ItemType.Key)
        {
            _character.InventoryCommandManager.ExecuteCommand(CharacterCommandType.Use, collidedObject.item);
            Debug.Log("key");
        }
        else
        {
            _character.InventoryCommandManager.ExecuteCommand(CharacterCommandType.Collect, collidedObject.item);
        }
    }

    public void HandleCollisionExit(IItem collidedObject)
    {
        
    }

    public void HandleCollisionStay(IItem collidedObject)
    {

    }
}