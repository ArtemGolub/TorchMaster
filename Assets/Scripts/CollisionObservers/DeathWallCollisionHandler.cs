using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallCollisionHandler : ICollisionHandler<IDeathWall>
{
    private Character _character;
    public DeathWallCollisionHandler(Character character)
    {
        _character = character;
    }
    
    public void HandleCollision(IDeathWall collidedObject)
    {
        _character.SM.ChancgeState(CharacterStateType.Death);
    }

    public void HandleCollisionExit(IDeathWall collidedObject)
    {
        
    }
}
