using UnityEngine;

public class FirePointCollisionHandler : ICollisionHandler<IFirePoint>
{
    private Character _character;
    public FirePointCollisionHandler(Character character)
    {
        _character = character;
    }
    public void HandleCollision(IFirePoint collidedObject)
    {
        var torch = _character.InventoryCommandManager.GetItem(ItemType.Torch);
        if (torch != null && !collidedObject.burned)
        {
            _character.InventoryCommandManager.ExecuteCommand(CharacterCommandType.Throw, torch);
            collidedObject.Burn();
        }
    }
}
