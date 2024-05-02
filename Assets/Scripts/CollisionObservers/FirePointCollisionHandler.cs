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
            _character.Components.animator.SetBool("isTorch", false);
            torch.FSM.ChangeState(ItemStateType.Used);
            collidedObject.Burn();
        }
    }

    public void HandleCollisionExit(IFirePoint collidedObject)
    {
        
    }
}
