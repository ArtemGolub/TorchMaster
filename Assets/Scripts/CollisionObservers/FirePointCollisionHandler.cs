using System;
using UnityEngine;

public class FirePointCollisionHandler : ICollisionHandler<IFirePoint>
{
    private Character _character;
    public event Action<IFirePoint, Item, Character> BurnFirePoint;
    
    public FirePointCollisionHandler(Character character)
    {
        _character = character;
    }
    public void HandleCollision(IFirePoint collidedObject)
    {
        var torch = _character.InventoryCommandManager.GetItem(ItemType.Torch);
        if (torch != null && !collidedObject.burned)
        {
            //_character.Components.animator.SetBool("isTorch", false);
            _character.SM.ChancgeState(CharacterStateType.Use);
            _character.AnimationEventHandler.HandleCollision(collidedObject, torch, _character);
            
            Vector3 direction = collidedObject._transform.position - _character.Components.characterTransform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            _character.Components.characterTransform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
            //_character.Components.characterTransform.LookAt(collidedObject._transform);
           // torch.FSM.ChangeState(ItemStateType.Used);
           // collidedObject.Burn();
        }
    }
    


    public void HandleCollisionExit(IFirePoint collidedObject)
    {
        
    }

    public void HandleCollisionStay(IFirePoint collidedObject)
    {
        
    }
}
