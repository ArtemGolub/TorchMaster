using UnityEngine;

public class DoorCollisionHandler : ICollisionHandler<IDoor>
{
    private Character _character;
    public DoorCollisionHandler(Character character)
    {
        _character = character;
    }
    
    public void HandleCollision(IDoor collidedObject)
    {
        collidedObject.TryOpen(_character.hasKey);
    }

    public void HandleCollisionExit(IDoor collidedObject)
    {
    }

    public void HandleCollisionStay(IDoor collidedObject)
    {
    }
}